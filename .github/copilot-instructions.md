# Copilot Instructions for detect-windows-version

## Project Overview

Determining the Windows version and edition can be challenging. The `System.Environment.OSVersion.Version` property in .NET Framework up to version 4.8.1 and .NET Core up to version 3.1 returns **incorrect results on Windows 10 and later versions**. Additionally, correctly identifying the Windows edition (e.g., distinguishing between Windows 10 and Windows 11) requires handling build numbers, which adds complexity.

This .NET Standard 2.0 library abstracts these challenges by detecting Windows versions (Windows 2000–Windows 11, Server 2003–Server 2022) and returning the result as a **strongly typed `OperatingSystem` enum**, eliminating ambiguous string representations. It works out of the box on Windows 11, Windows 10, Windows Server 2022, Windows Server 2019, and earlier versions.

**Why this library exists**: Without it, relying on `System.Environment.OSVersion.Version` will give incorrect major/minor versions on modern Windows. This library combines Win32 API calls (`RtlGetVersion`), system metrics, registry reads, and complex cascading detection rules to return accurate results.

## Architecture Essentials

### Core Design: Dependency Injection for Testability
- **Main API**: `OSVersion` static class with properties like `MajorVersion`, `MinorVersion`, `BuildNumber`, `IsWorkstation`, `Is64BitOperatingSystem`
- **Provider Pattern**: Three injected providers replace default implementations:
  - `IWin32API` — wraps unsafe `RtlGetVersion` call + `GetSystemMetrics` (see [Win32ApiProvider.cs](src/OSVersionExt/Win32API/Win32ApiProvider.cs))
  - `IEnvironment` — wraps `Environment.Is64BitOperatingSystem` (see [EnvironmentProvider.cs](src/OSVersionExt/Environment/EnvironmentProvider.cs))
  - `IRegistry` — reads registry values for Windows 10+ metadata (DisplayVersion, UBR) (see [RegistryProviderDefault.cs](src/OSVersionExt/MajorVersion10/RegistryProviderDefault.cs))
- Providers set via static methods: `OSVersion.SetWin32ApiProvider()`, `OSVersion.SetEnvironmentProvider()` (with null validation)

### Version Detection Logic
Detection combines **four signals** to distinguish similar versions:
1. **MajorVersion + MinorVersion** — e.g., (10, 0) = Win10/11/Server2016+, (6, 3) = Win8.1/Server2012R2
2. **BuildNumber** — differentiates Win11 (≥22000) from Win10, and Server2022 (≥20348) from Server2016
3. **IsWorkstation vs IsServer** — ProductType enum determines workstation or server
4. **System Metrics + SuiteMask** — legacy versions use `SM_SERVERR2` flag and bitwise-masked `SuiteMask` flags

See [windows-version-detection-rules.md](docs/windows-version-detection-rules.md) for all rules (matches cascade of if-else in [OSVersion.cs](src/OSVersionExt/OSVersion.cs#L75-L125)).

### Windows 10+ Special Handling
`OSVersion.MajorVersion10Properties()` returns struct with registry-read metadata:
- `DisplayVersion` — user-facing version string (e.g., "24H2"), read from `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion`
- `UBR` — Update Build Revision for patch tracking
- `ReleaseId` — legacy numeric ID (deprecated, Windows 10 only) + hidden behind `[Obsolete]` attribute

## File Organization
- **src/OSVersionExt/** — Main library
  - `OSVersion.cs` — Static public API
  - `VersionInfo.cs` — Simple version struct wrapping `System.Version`
  - **Win32API/** — P/Invoke wrappers + enums (`OSVERSIONINFOEX`, `NTSTATUS`, `ProductType`, `SuiteMask`, `SystemMetric`)
  - **MajorVersion10/** — Registry-based Windows 10+ properties
  - **Environment/** — Environment info abstraction
  - **Registry/** — Registry read abstraction
- **tests/OSVersionExtensionTests/** — MSTest unit tests (targets net481 + net8.0)
- **Example/** + **samples/** — Example applications using the library

## Build & Test Commands
```bash
# Build library (targets netstandard2.0)
dotnet build src/OSVersionExt/OSVersionExtension.csproj

# Build and run tests (MSTest)
dotnet test tests/OSVersionExtensionTests/OSVersionExtensionTests.csproj

# Pack NuGet package (version in .csproj: currently 4.0.0)
dotnet pack src/OSVersionExt/OSVersionExtension.csproj
```

## Key Patterns & Conventions

### Provider Injection & Mocking
Tests inject custom implementations of `IWin32API`, `IEnvironment`, `IRegistry` to mock system state without a real Windows system. Always validate provider null checks via `Assert.Throws<ArgumentNullException>()`.

### Registry Access
`MajorVersion10Properties` reads registry in constructor via injected `IRegistry`. If no provider is injected, defaults to `RegistryProviderDefault` which accesses the real registry:
```csharp
public MajorVersion10Properties(IRegistry registryProvider)
public MajorVersion10Properties()
```
On .NET Standard 2.0, depends on `Microsoft.Win32.Registry` NuGet package for registry access.

### Graceful Null Handling
Registry values return `null` if not found (e.g., `DisplayVersion` on older builds). Properties expose nullable strings; callers check for null.

### Deprecated Code
`ReleaseId` property marked `[Obsolete]` — still works but recommends `DisplayVersion` instead. Maintain deprecation messages during refactoring.

### SecurityCritical Attributes
`OSVersion` static constructor marked `[SecurityCritical]` due to Win32 interop; preserve this for security policy compliance.

## Integration Points
- **NuGet Package**: Published as `OSVersionExt` (see [OSVersionExtension.csproj](src/OSVersionExt/OSVersionExtension.csproj) package metadata)
- **External Dependency**: `Microsoft.Win32.Registry` (v5.0.0 on netstandard2.0 only)
- **GitHub Repo**: https://github.com/pruggitorg/detect-windows-version

## Testing Strategy
MSTest framework with focus on provider injection tests. Avoid assuming specific Windows version in unit tests — use mocked providers. Example:
```csharp
[TestMethod]
public void EnsureNullWin32ApiProviderThrows()
{
    Assert.Throws<ArgumentNullException>(() => OSVersion.SetWin32ApiProvider(null));
}
```

## Common Pitfalls
1. **Incorrect OSVersion on Win10+** — This library exists because .NET's built-in `System.Environment.OSVersion.Version` returns (6, 2) on Windows 10+; always use `OSVersion.GetOperatingSystem()` instead.
2. **Build Number Boundaries** — Win11/Server2022 detection pivots on specific build thresholds; verify bounds in [windows-version-detection-rules.md](docs/windows-version-detection-rules.md) before changing.
3. **Registry Access on Non-Windows** — `MajorVersion10Properties()` only safe on Win10+; throws `InvalidOperationException` if called on older versions.

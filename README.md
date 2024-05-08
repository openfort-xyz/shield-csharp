# Shield C# Library

ShieldCSharp is a TypeScript library for interacting with the Openfort Shield API. It provides easy-to-use methods for retrieving and storing secrets.

## Features
- Easy authentication with Openfort and custom authentication options.
- Methods for storing and retrieving secrets securely.

## Installation

.NET CLI
```shell
dotnet add package Shield.SDK
```

Package Manager
```shell
NuGet\Install-Package Shield.SDK
```

PackageReference
```xml
<PackageReference Include="Shield.SDK" />
```

## Usage

The package needs to be configured with your account's secret key, which is
available in the [Openfort Dashboard][api-keys]. Require it with the key's
value:

```cs
const shieldSDK = new ShieldSDK('your-api-key');
```

## Support

New features and bug fixes are released on the latest major version of the `openfort` package. If you are on an older major version, we recommend that you upgrade to the latest in order to use the new features and bug fixes including those for security vulnerabilities. Older major versions of the package will continue to be available for use, but will not be receiving any updates.

[api-keys]: https://dashboard.openfort.xyz/api-keys

<!--
# vim: set tw=79:
-->

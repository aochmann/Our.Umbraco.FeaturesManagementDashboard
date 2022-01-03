# Our.Umbraco.FeaturesManagementDashboard

[![Build](https://github.com/aochmann/Our.Umbraco.FeatureManagementDashboard/actions/workflows/build.yml/badge.svg?branch=develop)](https://github.com/aochmann/Our.Umbraco.FeatureManagementDashboard/actions/workflows/build.yml) [![NuGet release](https://img.shields.io/nuget/v/Our.Umbraco.FeaturesManagementDashboard.svg)](https://www.nuget.org/packages/Our.Umbraco.FeaturesManagementDashboard) [![NuGet](https://img.shields.io/nuget/dt/Our.Umbraco.FeaturesManagementDashboard.svg)](https://www.nuget.org/packages/Our.Umbraco.FeaturesManagementDashboard) [![Our Umbraco project page](https://img.shields.io/badge/our-umbraco-orange.svg)](https://our.umbraco.com/packages/backoffice-extensions/ourumbracofeaturesmanagementdashboard/) [![Umbraco version](https://img.shields.io/badge/umbraco->9.0.1-%233544b1)](https://github.com/aochmann/Our.Umbraco.FeatureManagementDashboard)

This package adds feature management dashboard into **Umbraco** backoffice. It use as feature management the Microsoft Feature Flags engine  - more information about [Microsoft Feature Flags Tutorial](https://docs.microsoft.com/en-us/azure/azure-app-configuration/use-feature-flags-dotnet-core?tabs=core5x).

## Getting Started 💫

This package is supported on Umbraco 9.0.1 and is available for `net5.0` and `net6.0`.

Umbraco backoffice credentials:
 * login: `admin@admin.com`
 * password: `adminadmin`

## Installation 🎊

`Our.Umbraco.FeaturesManagementDashboard` is available from: `NuGet`, `GitHub Packages` and `Our.Umbraco`.

#### NuGet

To [install from NuGet](https://www.nuget.org/packages/Our.Umbraco.FeaturesManagementDashboard), you can run one of the following commands:
 * NuGet Package Manage:

   ```powershell
    PM> Install-Package Our.Umbraco.FeatureManagementDashboard
   ```
 * Command line:

    ```powershell
    dotnet add [PROJECT.csproj] package Our.Umbraco.FeatureManagementDashboard
    ```

#### GitHub Package:

For now there isn't public token for installing from this repository. Please go into repository and go to **Packages** and download latest `nupkg` assets.

#### Our.Umbraco:

In backoffice search and install package.

## Usage 🔥

After installing the package you will need to enable package in `appsettings.json` and perform some features configuration.

To enable package create section with setting:

**appsettings.json**
```json
"FeaturesManagementDashboard": {
  "Enabled": true
}
```

Now we need to declare feature flags configurations that will be transferred into Umbraco.

Let's say we have following features:
 * `HomePage`
 * `WeatherPage`
 * `Weather`

The configuration for those flags will look in **appsettings.json** like this:

```json
"FeatureManagement": {
  "HomePage": true,
  "WeatherPage": false,
  "Weather": true
}
```

Currently it's only supported boolean value of features flags, in the future it will be extended to support full functionality that exists in `Microsoft.FeatureManagement` - more information: [feature flags declaration](https://docs.microsoft.com/en-us/azure/azure-app-configuration/use-feature-flags-dotnet-core?tabs=core5x#feature-flag-declaration).

After that all setting will be transferred into Umbraco - after that it will not override by default imported flags into Umbraco.

To override Umbraco feature flags lets add option in **appsettings.json**:

```json
"FeaturesManagementDashboard": {
  "Enabled": true,
  "Override": true
}
```

>**Note**: Override option is not required, it by default false.

After adding `Override: true` option it will each time override Umbraco feature flags configuration.

In backoffice there will be present `Feature Management Dashboard` under `Settings` section.

![Backoffice section](https://raw.githubusercontent.com/aochmann/Our.Umbraco.FeaturesManagementDashboard/develop/Docs/Images/backoffice_section.png)

#### Feature Flags configuration

There are few options to setup feature flags in code:
 1. MVC tag helper

    Install `Microsoft.FeatureManagement.AspNetCore` package into your Web application.

    In `Views\_ViewImports.cshtml` add helper import:
    ```cshtml
    @addTagHelper *, Microsoft.FeatureManagement.AspNetCore
    ```

    After that you can start working with feature flags on views by using:

    ```cshtml
    <feature name="featureName">
        <!-- Content -->
    </feature>
    ```

 1. Controller actions attribute

    Wrap your controller with attribute `[FeatureGate("WeatherPage")]`. Feature Flags engine will handle it automatically.

 1. Dependency injection

    Inject `IFeatureManager` service into your object. After that you can perform custom logic for working with features, for example:

    ```csharp
    public async Task<IViewComponentResult> InvokeAsync()
            => await _featureManager.IsEnabledAsync("Weather")
                ? View(new WeatherViewModel(_random.Next(-30, 30)))
                : Content(string.Empty);
    ```

 1. Other

    For other options and more information please see [Microsoft Feature Flags documentation](https://docs.microsoft.com/en-us/azure/azure-app-configuration/use-feature-flags-dotnet-core?tabs=core5x#middleware).

## Contribution guidelines ⛏

To raise a new bug, create an [issue on the GitHub repository](https://github.com/aochmann/Our.Umbraco.FeatureManagementDashboard/issues/new?assignees=&labels=&template=bug_report.md&title=). To fix a bug or add new features, fork the repository and send a pull request with your changes. Feel free to add [ideas to the repository's issues list](https://github.com/aochmann/Our.Umbraco.FeatureManagementDashboard/issues/new?assignees=&labels=&template=feature_request.md&title=) if you would to discuss anything related to the package.

## Changelog 📖

All notable changes to this project can be found in [CHANGELOG.md](CHANGELOG.md).

## Notes 📝

Future work:
 * adding more feature flags declaration types
 * adding adapters for custom integration with Umbraco functionalities, like [Matthew Wise feature flagging package](https://github.com/Matthew-Wise/feature-flagging-umbraco) #h5yr :raised_hands:

## License 📜

Licensed under the [MIT License](LICENSE.md).
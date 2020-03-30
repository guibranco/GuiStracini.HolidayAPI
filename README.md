# GuiStracini.HolidayAPI

A client wrapper of the [Holiday API](https://holidayapi.com/) for .NET projects (both Core & Framewok)

![HolidayAPI](https://raw.githubusercontent.com/guibranco/GuiStracini.HolidayAPI/master/logo.png)

## CI/CD

[![Build status](https://ci.appveyor.com/api/projects/status/2et11cwujyfnsruj?svg=true)](https://ci.appveyor.com/project/guibranco/2et11cwujyfnsruj)
[![GitHub last commit](https://img.shields.io/github/last-commit/guibranco/GuiStracini.HolidayAPI)](https://github.com/guibranco/GuiStracini.HolidayAPI)
[![Github last release](https://img.shields.io/github/release-date/guibranco/GuiStracini.HolidayAPI.svg?style=flat)](https://github.com/guibranco/GuiStracini.HolidayAPI)
[![GitHub license](https://img.shields.io/github/license/guibranco/GuiStracini.HolidayAPI)](https://github.com/guibranco/GuiStracini.HolidayAPI)

## Code Quality

[![Codacy Badge](https://api.codacy.com/project/badge/Grade/76b91975f0574a8399ec878494813f4d)](https://www.codacy.com/manual/guilherme_9/GuiStracini.HolidayAPI?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=guibranco/GuiStracini.HolidayAPI&amp;utm_campaign=Badge_Grade)
[![Codacy Badge](https://api.codacy.com/project/badge/Coverage/76b91975f0574a8399ec878494813f4d)](https://www.codacy.com/manual/guilherme_9/GuiStracini.HolidayAPI?utm_source=github.com&utm_medium=referral&utm_content=guibranco/GuiStracini.HolidayAPI&utm_campaign=Badge_Coverage)
[![codecov](https://codecov.io/gh/guibranco/GuiStracini.HolidayAPI/branch/master/graph/badge.svg)](https://codecov.io/gh/guibranco/GuiStracini.HolidayAPI)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=guibranco_GuiStracini.HolidayAPI&metric=alert_status)](https://sonarcloud.io/dashboard?id=guibranco_GuiStracini.HolidayAPI)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=guibranco_GuiStracini.HolidayAPI&metric=coverage)](https://sonarcloud.io/dashboard?id=guibranco_GuiStracini.HolidayAPI)

[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=guibranco_GuiStracini.HolidayAPI&metric=sqale_rating)](https://sonarcloud.io/dashboard?id=guibranco_GuiStracini.HolidayAPI)
[![Lines of Code](https://sonarcloud.io/api/project_badges/measure?project=guibranco_GuiStracini.HolidayAPI&metric=ncloc)](https://sonarcloud.io/dashboard?id=guibranco_GuiStracini.HolidayAPI)
[![Technical Debt](https://sonarcloud.io/api/project_badges/measure?project=guibranco_GuiStracini.HolidayAPI&metric=sqale_index)](https://sonarcloud.io/dashboard?id=guibranco_GuiStracini.HolidayAPI)
[![Duplicated Lines (%)](https://sonarcloud.io/api/project_badges/measure?project=guibranco_GuiStracini.HolidayAPI&metric=duplicated_lines_density)](https://sonarcloud.io/dashboard?id=guibranco_GuiStracini.HolidayAPI)

[![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=guibranco_GuiStracini.HolidayAPI&metric=reliability_rating)](https://sonarcloud.io/dashboard?id=guibranco_GuiStracini.HolidayAPI)
[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=guibranco_GuiStracini.HolidayAPI&metric=security_rating)](https://sonarcloud.io/dashboard?id=guibranco_GuiStracini.HolidayAPI)
[![Code Smells](https://sonarcloud.io/api/project_badges/measure?project=guibranco_GuiStracini.HolidayAPI&metric=code_smells)](https://sonarcloud.io/dashboard?id=guibranco_GuiStracini.HolidayAPI)
[![Bugs](https://sonarcloud.io/api/project_badges/measure?project=guibranco_GuiStracini.HolidayAPI&metric=bugs)](https://sonarcloud.io/dashboard?id=guibranco_GuiStracini.HolidayAPI)
[![Vulnerabilities](https://sonarcloud.io/api/project_badges/measure?project=guibranco_GuiStracini.HolidayAPI&metric=vulnerabilities)](https://sonarcloud.io/dashboard?id=guibranco_GuiStracini.HolidayAPI)

---

## Installation
[![GuiStracini.HolidayAPI NuGet Version](https://img.shields.io/nuget/v/GuiStracini.HolidayAPI.svg?style=flat)](https://www.nuget.org/packages/GuiStracini.HolidayAPI/)
[![GuiStracini.HolidayAPI NuGet Downloads](https://img.shields.io/nuget/dt/GuiStracini.HolidayAPI.svg?style=flat)](https://www.nuget.org/packages/GuiStracini.HolidayAPI/)
[![Github All Releases](https://img.shields.io/github/downloads/guibranco/GuiStracini.HolidayAPI/total.svg?style=flat)](https://github.com/guibranco/GuiStracini.HolidayAPI)

Download the latest zip file from the [Release pages](https://github.com/guibranco/GuiStracini.HolidayAPI/releases) or simple install from [NuGet](https://www.nuget.org/packages/GuiStracini.HolidayAPI) package manager

NuGet URL: https://www.nuget.org/packages/GuiStracini.HolidayAPI

NuGet installation via *Package Manager Console*:

```ps
Install-Package GuiStracini.HolidayAPI
```

---

## Features

Implements all features of Holiday API available at [HolidayAPI docs](https://holidayapi.com/)

  - Get holidays list (country code and year required)
  - Get filtered holidays (day, month, public, upcoming, previous, subdivisions, switch response language, search parameter)
  - Get countries list
  - Get filtered countries (search parameter)
  - Get languages list
  - Get filtered languages (search parameter)

---

## Usage

Get your API key at [Holiday API site](https://holidayapi.com/).

```cs

//Http Client - you should use your DI container for it
var client = HttpClientFactory.Create();
client.BaseAddress = new Uri("https://holidayapi.com/");
client.DefaultRequestHeaders.ExpectContinue = false;
client.DefaultRequestHeaders.Accept.Clear();
client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

//Use your API key
var myKey = "00000000-0000-0000-0000-000000000000";

//Instantiate a holidayApi client with your API key (GUID/UUID)
var holidayClient = new HolidayApiClient(myKey, client);

//Getting all holidays in Brazil for the last year:
var holidays = await holidayClient.GetHolidaysAsync("BR", DateTime.Now.Year-1, CancellationToken.None);
foreach(var holiday in holidays)
    Console.WriteLine("Holiday: {0} | Date: {1}", holiday.Name, holiday.Date);

//Getting all available countries
var countries = await holidayClient.GetCountries(CancellationToken.None);
foreach(var country in countries)
    Console.WriteLine("Country: {0} | Code: {1} | Flag: {2}", country.Name, country.Code, country.Flag);

//Getting all available languages
var languages = await holidayClient.GetLanguages(CancellationToken.None);
foreach(var language in languages)
    Console.WriteLine("Code: {0} | Name: {1}", language.Code, language.Name);
```

// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Style", "IDE0016:Use 'throw' expression", Justification = "Keeping validation-related code together improves readability", Scope = "member", Target = "~M:SpotHero_Backend_Challenge.VerifiedParkingRate.#ctor(System.String,System.Int32,System.Int32,System.Int32,System.Int32,System.TimeZoneInfo,System.Int32)")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Per casing in challenge requirements", Scope = "member", Target = "~P:SpotHero_Backend_Challenge.UnverifiedParkingRateInput.days")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Per casing in challenge requirements", Scope = "member", Target = "~P:SpotHero_Backend_Challenge.UnverifiedParkingRateInput.id")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Per casing in challenge requirements", Scope = "member", Target = "~P:SpotHero_Backend_Challenge.UnverifiedParkingRateInput.price")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Per casing in challenge requirements", Scope = "member", Target = "~P:SpotHero_Backend_Challenge.UnverifiedParkingRateInput.times")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Per casing in challenge requirements", Scope = "member", Target = "~P:SpotHero_Backend_Challenge.UnverifiedParkingRateInput.tz")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Per casing in challenge requirements", Scope = "member", Target = "~P:SpotHero_Backend_Challenge.Price.price")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Using camelCase for private properties and methods help developers be cognizant of scoping", Scope = "member", Target = "~M:SpotHero_Backend_Challenge.ParkingRateInstance.validateInputs(System.DateTime,System.DateTime,System.Int32)")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Using camelCase for private properties and methods help developers be cognizant of scoping", Scope = "member", Target = "~M:SpotHero_Backend_Challenge.RateMatcher.getRateInstances(System.DateTime)~System.Collections.Generic.List{SpotHero_Backend_Challenge.ParkingRateInstance}")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Using camelCase for private properties and methods help developers be cognizant of scoping", Scope = "member", Target = "~M:SpotHero_Backend_Challenge.VerifiedParkingRate.validateRate(SpotHero_Backend_Challenge.UnverifiedParkingRateInput)")]

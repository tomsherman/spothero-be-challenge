# SpotHero Backend Challenge

## Purpose

This code presents a solution to [SpotHero's backend challenge](https://github.com/spothero/be-code-challenge).

## Demo

This application is deployed as a Linux-based Docker container on Azure:

**Live demo: https://spothero-backend-challenge.azurewebsites.net/**

The app listens on the standard 443 HTTPS port because of Linux-on-Azure idiosyncrancies. If you wish to run it on port 5000, the easiest solution is to clone the repo and play the solution in Visual Studio 2019. 

## Design Choices

Per the specification, rates are represented as days of the week, a text-based range of hours (e.g. `0600-1800` for 6 AM to 6 PM), an [IANA timezone](https://www.iana.org/time-zones), and a price.

The application handles rate inputs--whether via the REST API or those retrieved from the MongoDB database--as _unverified_. When a new batch of rates is submitted, all must validate; if not, the entire update will fail.

Time is hard to model well, [especially when it involves timezones and the future](https://codeblog.jonskeet.uk/2019/03/27/storing-utc-is-not-a-silver-bullet/). Rates are stored with an associated timezone, which makes them future proof. However, the ISO-8601 timezone format does *not* require that a specific timezone be specified, but rather that the _offset_ must be specified. This offset might be correct at the time of booking future parking, but if the _timezone that created the offset changes_ (i.e. politicians change the rules), then a booking could become invalid. For these reasons, I recommend using the `price-epoch` `GET` endpoint, and when bookings are stored (out of scope for this challenge), I would store them as epoch times + IANA timezone.

## Tests

Tests are included in the `SpotHero_Backend_Challenge.Tests` project. They make heavy use of Theory tests in [Fluent Assertions](https://fluentassertions.com/), which is a nice and compact way to represent tests.

## A Note about Errors

The challenge [prescribes](https://github.com/spothero/be-code-challenge#response-requirements) that the API should respond with "unavailable" when invalid input is submitted. I was not sure how to interpret this requirement. This API returns:

  * A [`412`](https://httpstatuses.com/412) response when logically invalid inputs are provided
  * A [`404`](https://httpstatuses.com/404) response when inputs are valid but no parking rates meet the input criteria

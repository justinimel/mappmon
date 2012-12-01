#Google Maps API for .NET
Fast and lightweight client libraries for Google Maps API.

## Introduction
This is a fork of Luis Farzati's Google Maps API for .NET project on Codeplex at http://gmaps.codeplex.com. The original project hasn't been updated in over two years and I needed to make some tweaks to it for my own pet project.

Luis' original README is reproduced below, but the plans and roadmap it refers to are not my plans. At some point I might flesh this out further if there's interest.

---
# Luis' README
##Overview
This project intends to provide all the features available in the Google Maps API. It is being developed in C# for .NET Framework 3.5.

_Please note that this project is still in design and development phase; the libraries may suffer major changes even at the interface level, so don't rely (yet) in this software for production uses._

##Libraries
Designed with simplicity and extensibility in mind, there are different libraries according to what you need.

- Google.Api.Maps.Core Google.Api.Maps.Service is a low-level API client library, providing just the basic support for communicating with the API services through strong types.
- Google.Api.Maps (to be released in next version), works on top of the core library to provide a business-level interface as well as enhanced functionality such as distance calculations, HTML helpers, caching support and more.

## API Support
Currently the service library supports full coverage of the following Google Maps APIs:

- Geocoding
- Elevation
- Static Maps

## Quick Examples
Using Google Maps API for .NET is really easy, even if you decide to use the Service library.

### Getting an address from the Geocoding service
Let's suppose we want to search an address and get more information about it. We can write:

	var request = new GeocodingRequest();
	request.Address = "1600 Amphitheatre Parkway";
	request.Sensor = "false";
	var response = GeocodingService.GetResponse(request);

The _GeocodingService_ class submits the request to the API web service, and returns the response strongly typed as a GeocodingResponse object which may contain zero, one or more results. Assuming we received at least one result, let's get some of its properties:

	var result = response.Results.First();	
	Console.WriteLine("Full Address: " + result.FormattedAddress);         // "1600 Amphitheatre Pkwy, Mountain View, CA 94043, USA"
	Console.WriteLine("Latitude: " + result.Geometry.Location.Latitude);   // 37.4230180
	Console.WriteLine("Longitude: " + result.Geometry.Location.Longitude); // -122.0818530

### Getting a static map url
Static Maps support allows you to get a valid url which you can use, for example, with an <img src=""> tag.

	var map = new StaticMap();
	map.Center = "1000 7h Ave"; // or a lat/lng coordinate
	map.Zoom = "14";
	map.Size = "400x400";
	map.Sensor = "true";
	var url = map.ToUri();

Remember, the Service library is meant to be very lightweight and simple, with a very limited responsability. Consider it basically as a developer-friendly API wrapper. It doesn't validate any business rules or even API parameters.

If you want to use the APIs at a higher level, you may found useful the main business library which first alpha version is going to be released in the next few days.

##Roadmap
- Geocoding API
- Elevation API
- Static Maps (WORKING)
- Documentation! (WORKING)
- Sample code (WORKING)
- Higher level API classes (WORKING)
- Cacheability support (WORKING)
- HTML helpers
- Cache implementation
- Smart address search

## Contact
Questions, comments and/or suggestions are welcome! You can send me an email at luisfarzati@katulu.net, or you can follow me on Twitter: http://twitter.com/luisfarzati

**Thank you!**
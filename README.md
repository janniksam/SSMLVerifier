# SSMLVerifier
SSMLVerifier will verify that a given input is valid SSML.
It is based of a javascript implementation found here:
https://github.com/gsdriver/ssml-check

[![Build status master](https://ci.appveyor.com/api/projects/status/uyat18oaarhpwd50?svg=true&passingText=master%20-%20passing&failingText=master%20-%20failing&pendingText=master%20-%20pending)](https://ci.appveyor.com/project/janniksam/SSMLVerifier) 
[![Build status dev](https://ci.appveyor.com/api/projects/status/uyat18oaarhpwd50/branch/dev?svg=true&passingText=dev%20-%20passing&failingText=dev%20-%20failing&pendingText=dev%20-%20pending)](https://ci.appveyor.com/project/janniksam/SSMLVerifier/branch/dev)
[![NuGet version](https://badge.fury.io/nu/SSMLVerifier.svg)](https://badge.fury.io/nu/SSMLVerifier)

## WIP

This is a WIP project. Here is the current state of the implementation:

#### Common tags:

- ![#049b2c](https://placehold.it/15/049b2c/000000?text=+) audio
- ![#049b2c](https://placehold.it/15/049b2c/000000?text=+) break
- ![#049b2c](https://placehold.it/15/049b2c/000000?text=+) emphasis 
- ![#049b2c](https://placehold.it/15/049b2c/000000?text=+) p 
- ![#049b2c](https://placehold.it/15/049b2c/000000?text=+) prosody 
- ![#049b2c](https://placehold.it/15/049b2c/000000?text=+) s
- ![#049b2c](https://placehold.it/15/049b2c/000000?text=+) say-as
- ![#049b2c](https://placehold.it/15/049b2c/000000?text=+) speak
- ![#049b2c](https://placehold.it/15/049b2c/000000?text=+) sub

#### Amazon tags:

- ![#049b2c](https://placehold.it/15/049b2c/000000?text=+) amazon:effect
- ![#049b2c](https://placehold.it/15/049b2c/000000?text=+) amazon:emotion
- ![#049b2c](https://placehold.it/15/049b2c/000000?text=+) lang
- ![#049b2c](https://placehold.it/15/049b2c/000000?text=+) phoneme
- ![#c5f015](https://placehold.it/15/049b2c/000000?text=+) voice
- ![#049b2c](https://placehold.it/15/049b2c/000000?text=+) w

#### Google tags:

- ![#049b2c](https://placehold.it/15/049b2c/000000?text=+) par
- ![#049b2c](https://placehold.it/15/049b2c/000000?text=+) seq
- ![#049b2c](https://placehold.it/15/049b2c/000000?text=+) media
- ![#049b2c](https://placehold.it/15/049b2c/000000?text=+) desc

## Basic usage

The basic usage looks like this:

```cs
const string testSsml = "<speak>Hello <break strength='weak' time='1s'/> World!</speak>";
var verifier = new Verifer();
var result = verifier.Verify(testSsml);
if(result.State == VerificationState.Valid)
{
   Console.WriteLine("SSML is valid!");
}
else
{
   Console.WriteLine(result.Error);
}
```

But I'd recommend you to use the second parameter aswell, which sets the target platform.

```cs
verifier.Verify(testSsml, SsmlPlatform.Amazon);
// or ...
verifier.Verify(testSsml, SsmlPlatform.Google);
```

This way you are able to use platform specific tags like Amazon's `lang`-tag

```cs
// lang is an amazon-specific tag
const string testSsml = "<speak>Hello <lang xml:lang='de-DE'>Welt</lang></speak>";
var verifier = new Verifer();
// this will fail, because lang is a Amazon-specific tag
var result = verifier.Verify(testSsml, SsmlPlatform.Google); 
```

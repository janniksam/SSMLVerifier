# SSMLVerifier
SSMLVerifier will verify that a given input is valid SSML
It is based of a javascript implementation by found here:
https://github.com/gsdriver/ssml-check

## WIP

This is a WIP project. Here is the current state of the implementation:

#### Common tags:

- ![#f03c15](https://placehold.it/15/f03c15/000000?text=+) audio 
- ![#f03c15](https://placehold.it/15/f03c15/000000?text=+) break 
- ![#f03c15](https://placehold.it/15/f03c15/000000?text=+) emphasis 
- ![#f03c15](https://placehold.it/15/f03c15/000000?text=+) p 
- ![#f03c15](https://placehold.it/15/f03c15/000000?text=+) prosody 
- ![#f03c15](https://placehold.it/15/f03c15/000000?text=+) s
- ![#f03c15](https://placehold.it/15/f03c15/000000?text=+) say-as
- ![#f03c15](https://placehold.it/15/f03c15/000000?text=+) speak
- ![#f03c15](https://placehold.it/15/f03c15/000000?text=+) sub

#### Amazon tags:

- ![#049b2c](https://placehold.it/15/049b2c/000000?text=+) amazon:effects
- ![#049b2c](https://placehold.it/15/049b2c/000000?text=+) lang
- ![#049b2c](https://placehold.it/15/049b2c/000000?text=+) phoneme
- ![#c5f015](https://placehold.it/15/049b2c/000000?text=+) voice
- ![#049b2c](https://placehold.it/15/049b2c/000000?text=+) w

#### Google tags:

- ![#f03c15](https://placehold.it/15/f03c15/000000?text=+) par
- ![#f03c15](https://placehold.it/15/f03c15/000000?text=+) seq
- ![#f03c15](https://placehold.it/15/f03c15/000000?text=+) media

## Basic usage

The basic usage looks like this:

```cs
const string testSsml = "<speak>Hello <lang xml:lang='de-DE'>Welt</lang></speak>";
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

This way the only tags are valid, that will work on the set platform.

```cs
const string testSsml = "<speak>Hello <lang xml:lang='de-DE'>Welt</lang></speak>";
var verifier = new Verifer();
// this will fail, because lang is a Amazon-specific tag
var result = verifier.Verify(testSsml, SsmlPlatform.Google); 
```
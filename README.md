# Miki.Anilist
Anilist API wrapper

## Usage
```cs
AnilistClient client = new AnilistClient();
ICharacter miki = await client.GetCharacterAsync("Miki");
```

## Dependencies
- Miki.GraphQL
  - Miki.Rest
- Newtonsoft.Json

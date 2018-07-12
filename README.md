# Miki.Anilist
[![](https://img.shields.io/nuget/dt/Miki.Anilist.svg?style=for-the-badge)](https://www.nuget.org/packages/Miki.Anilist)
[![](https://img.shields.io/discord/259343729586864139.svg?style=for-the-badge&logo=discord)](https://discord.gg/XpG4kwE)

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

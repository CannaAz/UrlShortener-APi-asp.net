using Microsoft.EntityFrameworkCore;
using URLSHORTENER.Helpers;

namespace URLSHORTENER.Services;

public class UrlShorteningService
{
    private readonly AppDbContext _context;
    private readonly Random _random = new Random();
    public UrlShorteningService(AppDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task<string> GenerateUniqueCode()
    {
        while (true)
        {
            var _GenerateCode = GenerateCode(ShortLinkSettings.Length, ShortLinkSettings.Alphabet, _random);
            
            if (!await IsCodeUniqueAsync(_GenerateCode)) return _GenerateCode;
        }
    }

    public string GenerateCode(int length, string Alphabet, Random random)
    {
        var codeChars = new Char[length];
        int AlphabetLength = Alphabet.Length;

        for (int i = 0; i < length; i++)
        {
            var randomIdx = random.Next(AlphabetLength);
            codeChars[i] = Alphabet[randomIdx];
        }

        return new string(codeChars);
    }

    public async Task<bool> IsCodeUniqueAsync(string code)
    {
        return await _context.ShortenedUrl.AnyAsync(s => s.Code == code);
    }
}

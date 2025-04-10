using NanoidDotNet;

namespace Business.Helpers;

public static class IdGenerator
{
    
    public static string GenerateId(int idSize) 
    { 
        return Nanoid.Generate(size: idSize);
    }
    
}
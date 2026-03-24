using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Data
{
    public static class ModelBuilderExtension
    {
        public static PropertyBuilder<List<string>> ListConversion(this PropertyBuilder<List<string>> propertyBuilder)
        {
            return propertyBuilder.HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', System.StringSplitOptions.RemoveEmptyEntries).ToList()
            );
        }
    }
}

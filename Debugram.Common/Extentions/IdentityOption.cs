
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Security.Principal;


namespace Common.Utilities
{
    public static class IdentityOption
    {
        public static T To<T>(this IIdentity identity)
            where T : class, new()
        {
            var genericType = new T();
            var properties = genericType.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                .Where(n => n.CanWrite && n.CanRead & n.PropertyType != typeof(ICollection<>)).ToList();
            var claimIdentity = identity as ClaimsIdentity;
            var claimTypes = typeof(ClaimTypes).GetMembers();
            var AspClaimOption = typeof(ClaimsIdentityOptions).GetMembers();
            var listUsedClaim = new List<Claim>();
            foreach (var property in properties)
            {
                var convertionProp = property.Name.ToLowercaseNamingConvention();
                var claimEquals = claimTypes?.Where(n => n.Name.Like($"%{convertionProp[0]}%")).ToList();
                if (claimEquals.Count == 0)
                    claimEquals.AddRange(AspClaimOption?.Where(n => n.Name.Like($"%{convertionProp[0]}%")));

                if (convertionProp.Length > 1)
                    claimEquals?.AddRange(claimTypes?.Where(n => n.Name.Like($"%{convertionProp[1]}%")));

                var claimToIdentity = claimIdentity.Claims.Where(n =>
                claimEquals.Any(p => n.Type.Contains(p.Name.ToLower()) || claimEquals.Any(n => n.Name.Contains(property.Name))) && !listUsedClaim.Any(u => u.Type == n.Type));

                if (!claimToIdentity.Any())
                    continue;

                property.SetValue(genericType, property.CastPropertyValue(claimToIdentity.First().Value), null);
                listUsedClaim.Add(claimToIdentity.First());
            }
            return genericType;
        }

    }
}

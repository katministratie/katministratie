using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace Superkatten.Katministratie.ApiAuthentication.Services
{
    public class ApiKeyResult
    {
        [MemberNotNull(nameof(Claims))]
        public bool IsSuccess { get; }
        public IEnumerable<Claim>? Claims { get; }

        private ApiKeyResult(bool isSuccess, IEnumerable<Claim>? claims = null)
        {
            IsSuccess = isSuccess;
            Claims = claims;
        }

        public static ApiKeyResult Success(IEnumerable<Claim> claims)
        {
            return new(true, claims ?? throw new ArgumentNullException(nameof(claims)));
        }
        public static ApiKeyResult Fail()
        {
            return new(false);
        }

    }
}

// using System.Security.Claims;
// using Microsoft.AspNetCore.Mvc;

// namespace Exercise1.Api.Controllers.Attributes
// {
//     public class ClaimRequirementAttribute : TypeFilterAttribute {
//         public ClaimRequirementAttribute(string claimType, string claimValue) 
//             : base(typeof(ClaimRequirementFilter))
//         {
//             Arguments = new object[] {
//                 new Claim(claimType, claimValue)
//             };
//         }
//     }
// }
using System.Threading.Tasks;

namespace Api.Services
{
    public interface IAddressVerificationService
    {
        Task<bool> VerifyAddressAsync(string address);
        Task<bool> IsWithinDeliveryRangeAsync(string destinationAddress, double maxDistanceKm);
    }
}

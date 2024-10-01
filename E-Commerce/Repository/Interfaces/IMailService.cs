using E_Commerce.Models.VMs;

namespace E_Commerce.Repository.Interfaces
{
    public interface IMailService
    {
        bool SendMail(VmMailData Mail_Data);
    }
}

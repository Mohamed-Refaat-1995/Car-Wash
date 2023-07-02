using CarService.Models;

namespace CarService.Repository
{
    public interface IData
    {
        List<PreRequst> GetAll();
        void Insert(CallCenterData clientData);

        void Delete(int id);
    }
}

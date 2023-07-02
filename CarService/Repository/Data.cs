using CarService.Models;
using Microsoft.EntityFrameworkCore;


namespace CarService.Repository
{
    public class Data : IData
    {
        CarServiceEntities Context;

        public Data(CarServiceEntities Context)
        {
            this.Context = Context;
        }
        public void Delete(int id)
        {
            PreRequst ClientPreRequst = 
                Context.PreRequst.FirstOrDefault(m => m.Id == id);

            Context.PreRequst.Remove(ClientPreRequst);
            Context.SaveChanges();
        }
        
       

        public void Insert(CallCenterData clientData)
        {
            Context.CallCenterData.Add(clientData);
            Context.SaveChanges();
        }


        public List<PreRequst> GetAll()
        {
            return  Context.PreRequst.Include(c=>c.City).Include(c=>c.Services).ToList();
        }

        /*public List<ClientData> GetClientDataForCoordenator(string cityName)
        {
            return Context.ClientData.Where(e => e.City == cityName).ToList();
        }


        public ClientData GetById(int id)
        {
            var client = Context.ClientData.FirstOrDefault(c => c.Id == id);
            return (client);
        }


        public void CallCenterUpdate(int id, ClientData clientData)
        {
            ClientData client = 
                Context.ClientData.FirstOrDefault(e => e.Id == id);
            
            client.Name = clientData.Name; 
            client.CarType = clientData.CarType;
            client.Price = clientData.Price;
            client.Service = clientData.Service;
            client.ServiceDate = clientData.ServiceDate;
            client.OrderStatus = clientData.OrderStatus;
            client.Notes = clientData.Notes;


            Context.SaveChanges();
        }

        public void CoordenatiorUpdate(int id)
        {
            throw new NotImplementedException();
        }

        public void TechnicalUpdate(int id)
        {
            throw new NotImplementedException();
        }
*/
        
    }
}

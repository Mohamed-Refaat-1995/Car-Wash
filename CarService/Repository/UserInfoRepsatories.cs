using CarService.Models;

namespace CarService.Reposatories
{
	public class UserInfoRepsatories : IUserInfoRepsatories
	{
		CarServiceEntities Context;

		public UserInfoRepsatories(CarServiceEntities Context)
		{
			this.Context = Context;
		}

		public void Delete(int id)
		{
			throw new NotImplementedException();
		}

		public List<PreRequst> GetAll()
		{
			throw new NotImplementedException();
		}

		public PreRequst GetById(int id)
		{
			throw new NotImplementedException();
		}

		public void Insert(PreRequst preRequst)
		{
            
            Context.PreRequst.Add(preRequst);
			
			Context.SaveChanges();
		}

		public void Update(int id)
		{
			PreRequst requst = Context.PreRequst.FirstOrDefault(x => x.Id == id);
			requst.IsExist = false;
            Context.SaveChanges();
        }
	}
}

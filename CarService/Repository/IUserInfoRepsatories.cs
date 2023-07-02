using CarService.Models;

namespace CarService.Reposatories
{
	public interface IUserInfoRepsatories
	{
		List<PreRequst> GetAll();
		PreRequst GetById(int id);
		void Insert(PreRequst preRequst);
		void Update(int id);
		void Delete(int id);
	}
}

using CarService.Models;
using CarService.ViewModel;

namespace CarService.Repository
{
    public class RulesRepository: IRulesRepository
    {
        CarServiceEntities context;

        public RulesRepository(CarServiceEntities context)//inject
        {
            this.context = context;
        }


        public List<RulesInfoViewModel> GetAllRulesInfo()
        {
           List< RulesInfoViewModel> rulesInfoViewModel = new List<RulesInfoViewModel>();
            var roles =  context.Roles.Select(e => new{ id =e.Id, name = e.Name}).ToList();
            foreach (var rule in roles)
            {
                rulesInfoViewModel.Add(new RulesInfoViewModel { Id = rule.id, RulesName = rule.name});
                    
            }
            return rulesInfoViewModel;
        }



    }


}

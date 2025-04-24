using appweb1.Data;
using appweb1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _ctx;
        public CreateModel(AppDbContext ctx){
            _ctx=ctx;
        }

        [BindProperty]
        public Cliente Cliente {get;set;}
        public void OnGet(){}

        public IActionResult OnPost(){
            _ctx.Clientes.Add(Cliente);
            _ctx.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}

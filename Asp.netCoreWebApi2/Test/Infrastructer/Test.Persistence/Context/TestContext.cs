using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Test.Domain.Entites;
using Test.Domain.Entites.Common;

namespace Test.Persistence.Context
{
    public class TestContext:IdentityDbContext<User>
    {
        public TestContext(DbContextOptions options):base(options)
        {

        }

        #region DbSet ler
        public DbSet<Product> Products { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }


        //burası ne zamna tetıklenır bız nezaman savechangesAsync methodunu tetıklers ısek ozaman burası kayıttan once cecalısır ıslemler den sonrada en altta kaydeder
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)//soguda araya gırmemıze yarar //hangisini kullandıysak onu overıde etmelıyız
        {
            //ChangeTracker :Entityler uzerınden yapılan degısıklıklerı yada yenı eklenen verılerın yakalanmasını saglıyan propertydir .update operasyonşarında Track edılen verılerı yakalayıp elde etmemızı saglar.
            var datas = ChangeTracker.Entries<BaseEntiey>(); //base entıty uzerınde kılerı yakala degısıklık olanları 

            foreach (var data in datas) //degısıklıklerı donduk burada 
            {
                var _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreateDate = DateTime.UtcNow,  //yapılan ıslem ekleme ıslemı ıse burası calıscak 
                    EntityState.Modified => data.Entity.UpdateDate = DateTime.UtcNow, //ypılan ıslem guncelleme ıse bursı calısır 
                    _ => default // Diğer durumlar için hiçbir şey yapma  {} bu calısmaz c# 9 dan dusuk duan bu program o yuzden default dedik
                }; ;
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}

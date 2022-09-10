using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Model : Entity
{
    
    public string Name { get; set; }
    public int DailyPrice { get; set; }
    public string ImageUrl { get; set; }
    public int BrandId { get; set; }
    public Brand? Brand { get; set; }

    public Model()
    {
    }
    public Model(int id, string name, int dailyPrice, string ımageUrl, int brandId) : base(id)
    {
        Name = name;
        DailyPrice = dailyPrice;
        ImageUrl = ımageUrl;
        BrandId = brandId;
    }
}
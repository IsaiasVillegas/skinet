using System;
using Core.Entities;

namespace Core.Specifications;

public class ProductSpecification : BaseSpecification<Product>
{
  public ProductSpecification(ProductSpecParams specParams) : base(x =>
    (string.IsNullOrEmpty(specParams.Search) || x.Name.ToLower().Contains(specParams.Search)) &&
    (specParams.Brands.Count == 0 || specParams.Brands.Contains(x.Brand)) &&
    (specParams.Types.Count == 0 || specParams.Types.Contains(x.Type))
  )
  {
   ApplyPaging(specParams.PageSize * (specParams.PageIndex -1), specParams.PageSize);
   // 5 * ( 1 - 1) = 0, 5
   // 5 * ( 2 - 1) = 5, 5
   // 5 * ( 3 - 1) = 10, 5 saltamos 10, tomamos 5

   switch (specParams.Sort)
   {
    case "priceAsc":
      AddOrderBy(x => x.Price);
      break;
    case "priceDesc":
      AddOrderByDescending(x => x.Price);
      break;
    default:
      AddOrderBy(x => x.Name);
      break;
   } 
  }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShopPraktika.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductCountry
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public int ProductId { get; set; }
    
        public virtual Country Country { get; set; }
        public virtual Product Product { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FlowersSale.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ItemsOrder
    {
        public int id { get; set; }
        public int id_items { get; set; }
        public int id_order { get; set; }
    
        public virtual Items Items { get; set; }
        public virtual Order Order { get; set; }
    }
}

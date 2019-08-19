using System.Collections.Generic;
using BookStore.Model;
using BookStore.Model.Enum;

namespace BookStore.Services.Contracts
{
    public interface ISuppliersService
    {
        void Create(string name, decimal priceToHome, decimal priceToOffice);

        IEnumerable<Supplier> All();

        bool MakeDafault(int id);

        decimal GetDiliveryPrice(int supplierId, DeliveryType deliveryType);

        bool Delete(int id);

        void Edit(int id, string name, decimal priceToHome, decimal priceToOffice);

        Supplier GetSupplierById(int id);

        Supplier GetDefaultSupplier();
    }
}
    
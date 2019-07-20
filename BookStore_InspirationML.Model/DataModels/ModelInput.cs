//*****************************************************************************************
//*                                                                                       *
//* This is an auto-generated file by Microsoft ML.NET CLI (Command-Line Interface) tool. *
//*                                                                                       *
//*****************************************************************************************

using Microsoft.ML.Data;

namespace BookStore_InspirationML.Model.DataModels
{
    public class ModelInput
    {
        [ColumnName("book_title"), LoadColumn(0)]
        public string Book_title { get; set; }


        [ColumnName("book_publisheDate"), LoadColumn(1)]
        public string Book_publisheDate { get; set; }


        [ColumnName("book_details"), LoadColumn(2)]
        public string Book_details { get; set; }


        [ColumnName("book_isbn"), LoadColumn(3)]
        public string Book_isbn { get; set; }


        [ColumnName("book_price"), LoadColumn(4)]
        public float Book_price { get; set; }


    }
}

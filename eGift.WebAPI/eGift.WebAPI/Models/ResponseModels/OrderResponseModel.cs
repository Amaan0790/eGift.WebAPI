namespace eGift.WebAPI.Models.ResponseModels
{
    public class OrderResponseModel
    {
        #region Constructors

        public OrderResponseModel()
        {
            OrderModel = new OrderModel();
            OrderDetailList = new List<OrderDetailsModel>();
        }

        #endregion

        #region Reference View Models

        public OrderModel OrderModel { get; set; }

        #endregion

        #region Reference List View Models

        public List<OrderDetailsModel> OrderDetailList { get; set; }

        #endregion
    }
}

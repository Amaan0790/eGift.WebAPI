namespace eGift.WebAPI.Models.RequestModels
{
    public class OrderRequestModel
    {
        #region Constructors

        public OrderRequestModel()
        {
            OrderModel = new OrderModel();
            OrderDetailList = new List<OrderDetailsModel>();
        }

        #endregion

        #region Reference View Models

        public OrderModel OrderModel { get; set; }

        #endregion Data Models

        #region Reference List View Models

        public List<OrderDetailsModel> OrderDetailList { get; set; }

        #endregion
    }
}

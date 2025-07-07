using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;
using static Discount.Grpc.DiscountService;

namespace Discount.Grpc.Services
{
    public class DiscountService(DiscountContext discountContext, ILogger<DiscountService> logger) : DiscountServiceBase
    {
        // Implement the methods defined in the DiscountServiceBase class here.
        // For example, you can implement methods to get, create, update, or delete discounts.
        // This is just a placeholder implementation.
        public override async Task<couponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {

            var config = new TypeAdapterConfig();

            config.NewConfig<couponModel, Coupon>()
                  .Map(dest => dest.Amount, src => (int)src.DiscountAmount);

            config.NewConfig<Coupon, couponModel>()
                  .Map(dest => dest.DiscountAmount, src => (double)src.Amount);

            var coupon = request.Coupon.Adapt<Coupon>(config);

            if (coupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid coupon data"));

            discountContext.Coupons.Add(coupon);
            await discountContext.SaveChangesAsync();

            logger.LogInformation("Discount created for {ProductName}", coupon.ProductName);

            var couponModel = coupon.Adapt<couponModel>(config); // <-- use same config

            return couponModel;

        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = await discountContext.Coupons
                .FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
            if (coupon == null)
                throw new RpcException(new Status(StatusCode.NotFound, "Coupon not found"));
            discountContext.Coupons.Remove(coupon);
            await discountContext.SaveChangesAsync();
            logger.LogInformation("Discount deleted for {ProductName}", request.ProductName);
            return new DeleteDiscountResponse { Success = true };
        }

        public override async Task<couponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            //var config = new TypeAdapterConfig();

            //config.NewConfig<couponModel, Coupon>()
            //      .Map(dest => dest.Amount, src => (int)src.DiscountAmount);

            //config.NewConfig<Coupon, couponModel>()
            //      .Map(dest => dest.DiscountAmount, src => (double)src.Amount);
            //var coupon = await discountContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
            //if (coupon == null)
            //    coupon = new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount Available" };

            //logger.LogInformation("Discont recive {Productname}", request.ProductName);
            //var couponModel = coupon.Adapt<couponModel>(config);
            //return couponModel;
            var config = new TypeAdapterConfig();

            config.NewConfig<couponModel, Coupon>()
                  .Map(dest => dest.Amount, src => (int)src.DiscountAmount)
    .Map(dest => dest.Id, src => src.Id.ToString()) // ✅ Correct
                  .Map(dest => dest.ProductName, src => src.ProductName)
                  .Map(dest => dest.Description, src => src.Description);

            config.NewConfig<Coupon, couponModel>()
                  .Map(dest => dest.DiscountAmount, src => (double)src.Amount)
                  .Map(dest => dest.Id, src => src.Id.ToString())
                  .Map(dest => dest.ProductName, src => src.ProductName)
                  .Map(dest => dest.Description, src => src.Description);

            var coupon = await discountContext.Coupons
                .FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

            if (coupon == null)
                coupon = new Coupon { Id = 0, ProductName = "No Discount", Amount = 0, Description = "No Discount Available" };

            logger.LogInformation("Discount received for {ProductName}", request.ProductName);

            var couponModel = coupon.Adapt<couponModel>(config);
            return couponModel;
            //return base.GetDiscount(request, context);
        }

        public override async Task<couponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var config = new TypeAdapterConfig();

            config.NewConfig<couponModel, Coupon>()
                  .Map(dest => dest.Amount, src => (int)src.DiscountAmount);

            config.NewConfig<Coupon, couponModel>()
                  .Map(dest => dest.DiscountAmount, src => (double)src.Amount);
            var coupon = request.Coupon.Adapt<Coupon>(config);
            if (coupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid coupon data"));
            discountContext.Coupons.Update(coupon);
            await discountContext.SaveChangesAsync();
            logger.LogInformation("Discount updated for {ProductName}", coupon.ProductName);
            var couponModel = coupon.Adapt<couponModel>(config); // <-- use same config
            return couponModel;
            //return base.UpdateDiscount(request, context);
        }
    }
}

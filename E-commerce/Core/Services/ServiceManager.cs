﻿using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Services.Abstraction;
using Shared;

namespace Services
{
    public class ServiceManager : IServiceManager

    {
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<IBasketService> _basketService;
        private readonly Lazy<IAuthenticationService> _authenticationService;
        private readonly Lazy<IOrderService> _OrderService;
        private readonly Lazy<IPaymentService> _PaymentService;


        public ServiceManager(IUnitOfWork unitOfWork ,IMapper mapper , IBasketRepository basketRepository,
            UserManager<User> userManager, IOptions<JwtOptions>options, IConfiguration configuration)
        {

            _productService = new Lazy<IProductService>(() => new ProductService( unitOfWork, mapper ));
            _basketService = new Lazy<IBasketService>(() => new BasketService( basketRepository, mapper ));
            _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(userManager,options,mapper));
            _OrderService = new Lazy<IOrderService>(() => new OrderService(mapper, basketRepository, unitOfWork));
            _PaymentService = new Lazy<IPaymentService>(() => new PaymentService(basketRepository, unitOfWork, mapper, configuration));
        }
        public IProductService ProductService => _productService.Value;

        public IBasketService BasketService => _basketService.Value;

        public IAuthenticationService AuthenticationService => _authenticationService.Value ;

        public IOrderService OrderService => _OrderService.Value;

        public IPaymentService paymentService => _PaymentService.Value;
    }
}

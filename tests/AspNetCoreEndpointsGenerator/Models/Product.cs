﻿using SourceGeneratorsAttributes;

namespace AspNetCoreEndpointsGenerator.Models;

// NOTE: Can be accessed at `/product`
[RestApi]
public class Product
{
    public Guid Id { get; set; }
}
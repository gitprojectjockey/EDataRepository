USE [ProductDataRepository]
GO
/****** Object:  StoredProcedure [dbo].[spGetPagedProductsByCompany]    Script Date: 12/19/2016 12:09:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[spGetPagedProductsByCompany]
@CompanyName nvarchar(50),
@DisplayLength int,
@DisplayStart int,
@SortCol int,
@SortDir nvarchar(10),
@Search nvarchar(255) = NULL
as
begin
    Declare @FirstRec int, @LastRec int
    Set @FirstRec = @DisplayStart;
    Set @LastRec = @DisplayStart + @DisplayLength;
    
    With CTE_Product as
    (
         Select ROW_NUMBER() over (order by
         
         case when (@SortCol = 0 and @SortDir='asc')
             then ProductId
         end asc,
         case when (@SortCol = 0 and @SortDir='desc')
             then ProductId
         end desc,
         
        case when (@SortCol = 1 and @SortDir='asc')
             then ProductName
        end asc,
        case when (@SortCol = 1 and @SortDir='desc')
            then ProductName
        end desc,

        case when (@SortCol = 2 and @SortDir='asc')
            then Description
        end asc,
        case when (@SortCol = 2 and @SortDir='desc')
            then Description
        end desc,

        case when (@SortCol = 3 and @SortDir='asc')
            then Price
        end asc,
        case when (@SortCol = 3 and @SortDir='desc')
            then Price
        end desc)
         as RowNum,
         COUNT(*) over() as TotalCount,
         ProductId,
         ProductName,
		 CompanyName,
		 CategoryName,
         Description,
         Price,
         Product.CompanyId,
		 Product.ProductCategoryId
         from Product 
		 inner join Company
		 on Product.CompanyId = Company.CompanyId
		 inner join ProductCategory 
		 on Product.ProductCategoryId = ProductCategory.ProductCategoryId 
         where (@Search IS NULL
                 Or ProductId like '%' + @Search + '%'
                 Or ProductName like '%' + @Search + '%'
                 Or Description like '%' + @Search + '%'
                 Or Price like '%' + @Search + '%')
				
    )
    Select *
    from CTE_Product
    where RowNum > @FirstRec 
	and RowNum <= @LastRec
	and CompanyName = @CompanyName
end
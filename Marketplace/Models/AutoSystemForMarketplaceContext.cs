using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Models;

public partial class AutoSystemForMarketplaceContext : DbContext
{
    public AutoSystemForMarketplaceContext()
    {
    }

    public AutoSystemForMarketplaceContext(DbContextOptions<AutoSystemForMarketplaceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Byer> Byers { get; set; }

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<CategoryAttribute> CategoryAttributes { get; set; }

    public virtual DbSet<DictionaryStatusHistory> DictionaryStatusHistories { get; set; }

    public virtual DbSet<Discount> Discounts { get; set; }

    public virtual DbSet<EmployeeMarketplace> EmployeeMarketplaces { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<PresentCard> PresentCards { get; set; }

    public virtual DbSet<PresentCardDiscount> PresentCardDiscounts { get; set; }

    public virtual DbSet<ProductAttributeCategory> ProductAttributeCategories { get; set; }

    public virtual DbSet<ProductAttributeValue> ProductAttributeValues { get; set; }

    public virtual DbSet<ProductPlace> ProductPlaces { get; set; }

    public virtual DbSet<ProductPrice> ProductPrices { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<RoleEmployee> RoleEmployees { get; set; }

    public virtual DbSet<Seller> Sellers { get; set; }

    public virtual DbSet<StatusHistoryOrder> StatusHistoryOrders { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<TaskCompletionStatus> TaskCompletionStatuses { get; set; }

    public virtual DbSet<TaskHistory> TaskHistories { get; set; }

    public virtual DbSet<TitleDescription> TitleDescriptions { get; set; }

    public virtual DbSet<Warehouse> Warehouses { get; set; }

    public virtual DbSet<WorkLog> WorkLogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-R78OBKG\\SQLEXPRESS;Initial Catalog=AutoSystemForMarketplace;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Byer>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("PK__Byer__AB6E6165AFF11B40");

            entity.ToTable("Byer");

            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Patronomyc)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("patronomyc");
            entity.Property(e => e.PersonalDiscount)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("personal_discount");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone");
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.CartItemId).HasName("PK__Cart_Ite__5D9A6C6EA416E360");

            entity.ToTable("Cart_Item");

            entity.HasIndex(e => e.ByerId, "idx_cart_item_byer");

            entity.HasIndex(e => e.PresentCardId, "idx_cart_item_present_card");

            entity.Property(e => e.CartItemId).HasColumnName("cart_item_id");
            entity.Property(e => e.ByerId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("byer_id");
            entity.Property(e => e.PresentCardId).HasColumnName("present_card_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Byer).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.ByerId)
                .HasConstraintName("FK__Cart_Item__byer___34C8D9D1");

            entity.HasOne(d => d.PresentCard).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.PresentCardId)
                .HasConstraintName("FK__Cart_Item__prese__35BCFE0A");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__D54EE9B4B18B23A8");

            entity.ToTable("Category");

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<CategoryAttribute>(entity =>
        {
            entity.HasKey(e => e.AttributeId).HasName("PK__Category__9090C9BBE7C2E2A0");

            entity.ToTable("Category_Attribute");

            entity.Property(e => e.AttributeId).HasColumnName("attribute_id");
            entity.Property(e => e.DataType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("data_type");
            entity.Property(e => e.NameAttribute)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name_attribute");
            entity.Property(e => e.Unit)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("unit");
        });

        modelBuilder.Entity<DictionaryStatusHistory>(entity =>
        {
            entity.HasKey(e => e.DictionaryStatusHistoryId).HasName("PK__Dictiona__8854329F7829D25C");

            entity.ToTable("Dictionary_Status_History");

            entity.Property(e => e.DictionaryStatusHistoryId).HasColumnName("dictionary_status_history_id");
            entity.Property(e => e.StatusName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("status_name");
        });

        modelBuilder.Entity<Discount>(entity =>
        {
            entity.HasKey(e => e.DiscountId).HasName("PK__Discount__BDBE9EF934361D44");

            entity.ToTable("Discount");

            entity.Property(e => e.DiscountId).HasColumnName("discount_id");
            entity.Property(e => e.DiscountAmount)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("discount_amount");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.NameDiscount)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name_discount");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
        });

        modelBuilder.Entity<EmployeeMarketplace>(entity =>
        {
            entity.HasKey(e => e.EmployeeMarketplaceId).HasName("PK__Employee__8B4B7FEC258D0579");

            entity.ToTable("Employee_Marketplace");

            entity.HasIndex(e => e.Email, "UQ__Employee__AB6E616422E60F52").IsUnique();

            entity.Property(e => e.EmployeeMarketplaceId).HasColumnName("employee_marketplace_id");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("date_created");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Patronymic)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("patronymic");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.WarehouseId).HasColumnName("warehouse_id");

            entity.HasOne(d => d.Role).WithMany(p => p.EmployeeMarketplaces)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Employee___role___6C190EBB");

            entity.HasOne(d => d.Warehouse).WithMany(p => p.EmployeeMarketplaces)
                .HasForeignKey(d => d.WarehouseId)
                .HasConstraintName("FK__Employee___wareh__6D0D32F4");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__46596229337C0B2B");

            entity.HasIndex(e => e.ByerId, "idx_orders_byer");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.ByerId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("byer_id");
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("order_date");
            entity.Property(e => e.TotalAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total_amount");

            entity.HasOne(d => d.Byer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ByerId)
                .HasConstraintName("FK__Orders__byer_id__398D8EEE");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.OrderItemsId).HasName("PK__Order_It__75291555E7EECF2E");

            entity.ToTable("Order_Items");

            entity.HasIndex(e => e.CartItemId, "idx_order_items_cart_item");

            entity.HasIndex(e => e.OrderId, "idx_order_items_order");

            entity.Property(e => e.OrderItemsId).HasColumnName("order_items_id");
            entity.Property(e => e.CartItemId).HasColumnName("cart_item_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.StatusBoughtOut)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("pending")
                .HasColumnName("status_bought_out");

            entity.HasOne(d => d.CartItem).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.CartItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Order_Ite__cart___3E52440B");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__Order_Ite__order__3D5E1FD2");
        });

        modelBuilder.Entity<PresentCard>(entity =>
        {
            entity.HasKey(e => e.PresentCardId).HasName("PK__Present___90C86C0981B7F26F");

            entity.ToTable("Present_Card");

            entity.Property(e => e.PresentCardId).HasColumnName("present_card_id");
            entity.Property(e => e.Brand)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("brand");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Images)
                .HasColumnType("text")
                .HasColumnName("images");
            entity.Property(e => e.IsAvailable).HasColumnName("is_available");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.SellerId).HasColumnName("seller_id");

            entity.HasOne(d => d.Seller).WithMany(p => p.PresentCards)
                .HasForeignKey(d => d.SellerId)
                .HasConstraintName("FK__Present_C__selle__30F848ED");
        });

        modelBuilder.Entity<PresentCardDiscount>(entity =>
        {
            entity.HasKey(e => e.PresentCardDiscountId).HasName("PK__Present___596F219407898A2B");

            entity.ToTable("Present_Card_Discount");

            entity.Property(e => e.PresentCardDiscountId).HasColumnName("present_card_discount_id");
            entity.Property(e => e.DiscountId).HasColumnName("discount_id");
            entity.Property(e => e.PresentCardId).HasColumnName("present_card_id");

            entity.HasOne(d => d.Discount).WithMany(p => p.PresentCardDiscounts)
                .HasForeignKey(d => d.DiscountId)
                .HasConstraintName("FK__Present_C__disco__5EBF139D");

            entity.HasOne(d => d.PresentCard).WithMany(p => p.PresentCardDiscounts)
                .HasForeignKey(d => d.PresentCardId)
                .HasConstraintName("FK__Present_C__prese__5DCAEF64");
        });

        modelBuilder.Entity<ProductAttributeCategory>(entity =>
        {
            entity.HasKey(e => e.AttributeCategoryId).HasName("PK__Product___96840F0902BBDD03");

            entity.ToTable("Product_Attribute_Category");

            entity.Property(e => e.AttributeCategoryId).HasColumnName("attribute_category_id");
            entity.Property(e => e.AttributeId).HasColumnName("attribute_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");

            entity.HasOne(d => d.Attribute).WithMany(p => p.ProductAttributeCategories)
                .HasForeignKey(d => d.AttributeId)
                .HasConstraintName("FK__Product_A__attri__5629CD9C");

            entity.HasOne(d => d.Category).WithMany(p => p.ProductAttributeCategories)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Product_A__categ__571DF1D5");
        });

        modelBuilder.Entity<ProductAttributeValue>(entity =>
        {
            entity.HasKey(e => e.AttributeValueId).HasName("PK__Product___C817A2F67DE5D486");

            entity.ToTable("Product_Attribute_Value");

            entity.HasIndex(e => e.PresentCardId, "idx_product_attribute_value_present_card");

            entity.Property(e => e.AttributeValueId).HasColumnName("attribute_value_id");
            entity.Property(e => e.AttributeCategoryId).HasColumnName("attribute_category_id");
            entity.Property(e => e.PresentCardId).HasColumnName("present_card_id");
            entity.Property(e => e.Value)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("value");

            entity.HasOne(d => d.AttributeCategory).WithMany(p => p.ProductAttributeValues)
                .HasForeignKey(d => d.AttributeCategoryId)
                .HasConstraintName("FK__Product_A__attri__5AEE82B9");

            entity.HasOne(d => d.PresentCard).WithMany(p => p.ProductAttributeValues)
                .HasForeignKey(d => d.PresentCardId)
                .HasConstraintName("FK__Product_A__prese__59FA5E80");
        });

        modelBuilder.Entity<ProductPlace>(entity =>
        {
            entity.HasKey(e => e.ProductPlaceId).HasName("PK__Product___ACBE7B9228D8683E");

            entity.ToTable("Product_Place");

            entity.HasIndex(e => e.PresentCardId, "idx_product_place_present_card");

            entity.HasIndex(e => e.IdWarehouse, "idx_product_place_warehouse");

            entity.Property(e => e.ProductPlaceId).HasColumnName("product_place_id");
            entity.Property(e => e.IdWarehouse).HasColumnName("id_warehouse");
            entity.Property(e => e.PresentCardId).HasColumnName("present_card_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Shelf)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("shelf");
            entity.Property(e => e.Shelving)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("shelving");

            entity.HasOne(d => d.IdWarehouseNavigation).WithMany(p => p.ProductPlaces)
                .HasForeignKey(d => d.IdWarehouse)
                .HasConstraintName("FK__Product_P__id_wa__4E88ABD4");

            entity.HasOne(d => d.PresentCard).WithMany(p => p.ProductPlaces)
                .HasForeignKey(d => d.PresentCardId)
                .HasConstraintName("FK__Product_P__prese__4D94879B");
        });

        modelBuilder.Entity<ProductPrice>(entity =>
        {
            entity.HasKey(e => e.ProductPriceId).HasName("PK__Product___DC88EB6124B54353");

            entity.ToTable("Product_Price");

            entity.HasIndex(e => e.PresentCardId, "idx_product_price_present_card");

            entity.Property(e => e.ProductPriceId).HasColumnName("product_price_id");
            entity.Property(e => e.PresentCardId).HasColumnName("present_card_id");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");

            entity.HasOne(d => d.PresentCard).WithMany(p => p.ProductPrices)
                .HasForeignKey(d => d.PresentCardId)
                .HasConstraintName("FK__Product_P__prese__5165187F");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.QuestionsId).HasName("PK__Question__1CEDF945C670721A");

            entity.HasIndex(e => e.ByerId, "idx_questions_byer");

            entity.HasIndex(e => e.PresentCardId, "idx_questions_present_card");

            entity.Property(e => e.QuestionsId).HasColumnName("questions_id");
            entity.Property(e => e.AnswerSeller)
                .HasColumnType("text")
                .HasColumnName("answer_seller");
            entity.Property(e => e.ByerId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("byer_id");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("date_created");
            entity.Property(e => e.PresentCardId).HasColumnName("present_card_id");
            entity.Property(e => e.QuestionsByer)
                .HasColumnType("text")
                .HasColumnName("questions_byer");

            entity.HasOne(d => d.Byer).WithMany(p => p.Questions)
                .HasForeignKey(d => d.ByerId)
                .HasConstraintName("FK__Questions__byer___4222D4EF");

            entity.HasOne(d => d.PresentCard).WithMany(p => p.Questions)
                .HasForeignKey(d => d.PresentCardId)
                .HasConstraintName("FK__Questions__prese__4316F928");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewsId).HasName("PK__Reviews__B79F9E637A1D570D");

            entity.HasIndex(e => e.OrderItemsId, "idx_reviews_order_items");

            entity.Property(e => e.ReviewsId).HasColumnName("reviews_id");
            entity.Property(e => e.AnswerSeller)
                .HasColumnType("text")
                .HasColumnName("answer_seller");
            entity.Property(e => e.CommentByer)
                .HasColumnType("text")
                .HasColumnName("comment_byer");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("date_created");
            entity.Property(e => e.Estimation).HasColumnName("estimation");
            entity.Property(e => e.OrderItemsId).HasColumnName("order_items_id");

            entity.HasOne(d => d.OrderItems).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.OrderItemsId)
                .HasConstraintName("FK__Reviews__order_i__47DBAE45");
        });

        modelBuilder.Entity<RoleEmployee>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role_Emp__760965CC9818613B");

            entity.ToTable("Role_Employee");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<Seller>(entity =>
        {
            entity.HasKey(e => e.SellerId).HasName("PK__Seller__780A0A971AAA325E");

            entity.ToTable("Seller");

            entity.HasIndex(e => e.Email, "UQ__Seller__AB6E6164265A6AD6").IsUnique();

            entity.Property(e => e.SellerId).HasColumnName("seller_id");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("company_name");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Patronomcy)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("patronomcy");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone");
        });

        modelBuilder.Entity<StatusHistoryOrder>(entity =>
        {
            entity.HasKey(e => e.StatusHistoryOrderId).HasName("PK__Status_H__590104963D1224D0");

            entity.ToTable("Status_History_Order");

            entity.Property(e => e.StatusHistoryOrderId).HasColumnName("status_history_order_id");
            entity.Property(e => e.DataEdit)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("data_edit");
            entity.Property(e => e.DictionaryStatusHistoryId).HasColumnName("dictionary_status_history_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");

            entity.HasOne(d => d.DictionaryStatusHistory).WithMany(p => p.StatusHistoryOrders)
                .HasForeignKey(d => d.DictionaryStatusHistoryId)
                .HasConstraintName("FK__Status_Hi__dicti__6477ECF3");

            entity.HasOne(d => d.Order).WithMany(p => p.StatusHistoryOrders)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__Status_Hi__order__656C112C");
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("PK__Task__0492148D7DC5FAC5");

            entity.ToTable("Task");

            entity.HasIndex(e => e.WarehouseId, "idx_task_warehouse");

            entity.Property(e => e.TaskId).HasColumnName("task_id");
            entity.Property(e => e.ActualHourseWork)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("actual_hourse_work");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("date_created");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Priority)
                .HasDefaultValue(1)
                .HasColumnName("priority");
            entity.Property(e => e.WarehouseId).HasColumnName("warehouse_id");

            entity.HasOne(d => d.Order).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Task__order_id__72C60C4A");

            entity.HasOne(d => d.Warehouse).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.WarehouseId)
                .HasConstraintName("FK__Task__warehouse___71D1E811");
        });

        modelBuilder.Entity<TaskCompletionStatus>(entity =>
        {
            entity.HasKey(e => e.StatusTaskId).HasName("PK__Task_Com__136782FEDD5D1E74");

            entity.ToTable("Task_Completion_Status");

            entity.Property(e => e.StatusTaskId).HasColumnName("status_task_id");
            entity.Property(e => e.DescriptionId).HasColumnName("description_id");
            entity.Property(e => e.TitleStatus)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("title_status");

            entity.HasOne(d => d.Description).WithMany(p => p.TaskCompletionStatuses)
                .HasForeignKey(d => d.DescriptionId)
                .HasConstraintName("FK__Task_Comp__descr__778AC167");
        });

        modelBuilder.Entity<TaskHistory>(entity =>
        {
            entity.HasKey(e => e.HistoryId).HasName("PK__Task_His__096AA2E96CE7B7F1");

            entity.ToTable("Task_History");

            entity.HasIndex(e => e.TaskId, "idx_task_history_task");

            entity.Property(e => e.HistoryId).HasColumnName("history_id");
            entity.Property(e => e.ChangedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("changed_at");
            entity.Property(e => e.EmployeeMarketplaceId).HasColumnName("employee_marketplace_id");
            entity.Property(e => e.SellerId).HasColumnName("seller_id");
            entity.Property(e => e.StatusTaskId).HasColumnName("status_task_id");
            entity.Property(e => e.TaskId).HasColumnName("task_id");

            entity.HasOne(d => d.EmployeeMarketplace).WithMany(p => p.TaskHistories)
                .HasForeignKey(d => d.EmployeeMarketplaceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Task_Hist__emplo__7D439ABD");

            entity.HasOne(d => d.Seller).WithMany(p => p.TaskHistories)
                .HasForeignKey(d => d.SellerId)
                .HasConstraintName("FK__Task_Hist__selle__7E37BEF6");

            entity.HasOne(d => d.StatusTask).WithMany(p => p.TaskHistories)
                .HasForeignKey(d => d.StatusTaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Task_Hist__statu__7C4F7684");

            entity.HasOne(d => d.Task).WithMany(p => p.TaskHistories)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Task_Hist__task___7B5B524B");
        });

        modelBuilder.Entity<TitleDescription>(entity =>
        {
            entity.HasKey(e => e.DescriptionId).HasName("PK__Title_De__DF380AEAC8BA0D1E");

            entity.ToTable("Title_Description");

            entity.Property(e => e.DescriptionId).HasColumnName("description_id");
            entity.Property(e => e.TitleDescription1)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("title_description");
        });

        modelBuilder.Entity<Warehouse>(entity =>
        {
            entity.HasKey(e => e.WarehouseId).HasName("PK__Warehous__734FE6BF333CEF35");

            entity.ToTable("Warehouse");

            entity.Property(e => e.WarehouseId).HasColumnName("warehouse_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Type)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("type");
        });

        modelBuilder.Entity<WorkLog>(entity =>
        {
            entity.HasKey(e => e.WorkLogId).HasName("PK__Work_Log__07D586E060443EFB");

            entity.ToTable("Work_Log");

            entity.HasIndex(e => e.UserId, "idx_work_log_user");

            entity.Property(e => e.WorkLogId).HasColumnName("work_log_id");
            entity.Property(e => e.HoursSpent)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("hours_spent");
            entity.Property(e => e.QuantityTask).HasColumnName("quantity_task");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.WorkDate).HasColumnName("work_date");

            entity.HasOne(d => d.User).WithMany(p => p.WorkLogs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Work_Log__user_i__01142BA1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

using Second.Classlib.Sixth.Models;
using Second.Classlib.Sixth.Repositories;

namespace Second.Winforms.Sixth;

public partial class MainForm : Form
{
    private readonly SuppliersRepository _supplierRepo;
    private readonly MaterialsRepository _materialRepo;
    private readonly DeliveriesRepository _deliveryRepo;

    private DataGridView _gridSuppliers = new() { Dock = DockStyle.Fill, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
    private DataGridView _gridMaterials = new() { Dock = DockStyle.Fill, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
    private DataGridView _gridDeliveries = new() { Dock = DockStyle.Fill, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };

    public MainForm(string connectionString)
    {
        _supplierRepo = new SuppliersRepository(connectionString);
        _materialRepo = new MaterialsRepository(connectionString);
        _deliveryRepo = new DeliveriesRepository(connectionString);

        InitializeComponent();
        SetupLayout();
        RefreshAll();
    }

    private void SetupLayout()
    {
        Text = "Склад металлопроката - Управление";
        Size = new Size(1000, 600);

        var tabControl = new TabControl { Dock = DockStyle.Fill };
        
        tabControl.TabPages.Add(CreateTabPage("Поставщики", _gridSuppliers, OnAddSupplier, OnUpdateSupplier, OnDeleteSupplier));
        tabControl.TabPages.Add(CreateTabPage("Материалы", _gridMaterials, OnAddMaterial, OnUpdateMaterial, OnDeleteMaterial));
        tabControl.TabPages.Add(CreateTabPage("Поставки", _gridDeliveries, OnAddDelivery, OnUpdateDelivery, OnDeleteDelivery));

        Controls.Add(tabControl);
    }

    private TabPage CreateTabPage(string title, DataGridView grid, Action onAdd, Action onUpd, Action onDel)
    {
        var page = new TabPage(title);
        var panel = new Panel { Dock = DockStyle.Top, Height = 40 };
        
        var btnAdd = new Button { Text = "Добавить", Left = 10, Top = 5 };
        btnAdd.Click += (s, e) => onAdd();
        
        var btnUpd = new Button { Text = "Изменить", Left = 100, Top = 5 };
        btnUpd.Click += (s, e) => onUpd();

        var btnDel = new Button { Text = "Удалить", Left = 190, Top = 5 };
        btnDel.Click += (s, e) => onDel();

        panel.Controls.AddRange([btnAdd, btnUpd, btnDel]);
        page.Controls.Add(grid);
        page.Controls.Add(panel);
        return page;
    }

    private void RefreshAll()
    {
        _gridSuppliers.DataSource = _supplierRepo.GetAll().ToList();
        _gridMaterials.DataSource = _materialRepo.GetAll().ToList();
        _gridDeliveries.DataSource = _deliveryRepo.GetAll().ToList();
    }

    private void OnAddSupplier()
    {
        string name = Microsoft.VisualBasic.Interaction.InputBox("Имя поставщика:", "Добавление");
        if (!string.IsNullOrWhiteSpace(name)) {
            _supplierRepo.Create(new Supplier { Name = name });
            RefreshAll();
        }
    }

    private void OnUpdateSupplier()
    {
        if (_gridSuppliers.CurrentRow?.DataBoundItem is Supplier s) {
            string name = Microsoft.VisualBasic.Interaction.InputBox("Новое имя:", "Изменение", s.Name);
            if (!string.IsNullOrWhiteSpace(name)) {
                s.Name = name;
                _supplierRepo.Update(s);
                RefreshAll();
            }
        }
    }

    private void OnDeleteSupplier()
    {
        if (_gridSuppliers.CurrentRow?.DataBoundItem is Supplier s) {
            _supplierRepo.Delete(s.Id);
            RefreshAll();
        }
    }
    private void OnAddMaterial()
    {
        string name = Microsoft.VisualBasic.Interaction.InputBox("Название материала:", "Добавление");
        if (!string.IsNullOrWhiteSpace(name)) {
            _materialRepo.Create(new Material { Name = name });
            RefreshAll();
        }
    }

    private void OnDeleteMaterial() { if (_gridMaterials.CurrentRow?.DataBoundItem is Material m) { _materialRepo.Delete(m.Id); RefreshAll(); } }

    private void OnAddDelivery()
    {
        string sId = Microsoft.VisualBasic.Interaction.InputBox("ID Поставщика:", "Поставка");
        string mId = Microsoft.VisualBasic.Interaction.InputBox("ID Материала:", "Поставка");
        if (int.TryParse(sId, out var sid) && int.TryParse(mId, out var mid)) {
            _deliveryRepo.Create(new Delivery { SupplierId = sid, MaterialId = mid, DeliveryDate = DateTime.Now });
            RefreshAll();
        }
    }

    private void OnDeleteDelivery() { if (_gridDeliveries.CurrentRow?.DataBoundItem is Delivery d) { _deliveryRepo.Delete(d.Id); RefreshAll(); } }

    private void OnUpdateMaterial()
    {
        if (_gridMaterials.CurrentRow?.DataBoundItem is Material m)
        {
            string name = Microsoft.VisualBasic.Interaction.InputBox("Новое название материала:", "Изменение", m.Name);
            if (!string.IsNullOrWhiteSpace(name))
            {
                m.Name = name;
                _materialRepo.Update(m);
                RefreshAll();
            }
        }
    }

    private void OnUpdateDelivery()
    {
        if (_gridDeliveries.CurrentRow?.DataBoundItem is Delivery d)
        {
            string sId = Microsoft.VisualBasic.Interaction.InputBox("Новый ID Поставщика:", "Изменение поставки", d.SupplierId.ToString());
            string mId = Microsoft.VisualBasic.Interaction.InputBox("Новый ID Материала:", "Изменение поставки", d.MaterialId.ToString());

            if (int.TryParse(sId, out var sid) && int.TryParse(mId, out var mid))
            {
                d.SupplierId = sid;
                d.MaterialId = mid;
                _deliveryRepo.Update(d);
                RefreshAll();
            }
            else if (!string.IsNullOrEmpty(sId) || !string.IsNullOrEmpty(mId))
            {
                MessageBox.Show("ID должны быть числами!", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private void InitializeComponent() { }
}

// API Base URLs for the three separate services
const SELLER_API = 'http://localhost:5010/api';
const DISTRIBUTOR_API = 'http://localhost:5020/api';
const MANUFACTURER_API = 'http://localhost:5030/api';

// Global State
let currentTab = 'seller';
let blanketModels = [];
let customers = [];
let sellers = [];
let distributors = [];

let currentSellerId = null;
let currentDistributorId = null;

// Initialize App
document.addEventListener('DOMContentLoaded', async () => {
    showToast('Initializing', 'Connecting to SOA Grid services...', 'fa-circle-info');
    await refreshAllData();
});

// Toast Notification
function showToast(title, message, icon = 'fa-circle-info', type = 'info') {
    const toast = document.getElementById('toast-notification');
    const toastTitle = document.getElementById('toast-title');
    const toastMsg = document.getElementById('toast-message');
    const toastIcon = document.getElementById('toast-icon');

    toastTitle.textContent = title;
    toastMsg.textContent = message;
    
    // Set icon class
    toastIcon.className = `fa-solid ${icon}`;
    
    // Set borders/colors based on type
    if (type === 'success') {
        toast.style.borderColor = 'var(--accent-success)';
        toastIcon.style.color = 'var(--accent-success)';
    } else if (type === 'warning') {
        toast.style.borderColor = 'var(--accent-warning)';
        toastIcon.style.color = 'var(--accent-warning)';
    } else if (type === 'danger') {
        toast.style.borderColor = 'var(--accent-danger)';
        toastIcon.style.color = 'var(--accent-danger)';
    } else {
        toast.style.borderColor = 'var(--accent-purple)';
        toastIcon.style.color = 'var(--accent-purple)';
    }

    toast.classList.add('show');

    setTimeout(() => {
        toast.classList.remove('show');
    }, 4500);
}

// Switch tabs between services
function switchTab(tabName) {
    currentTab = tabName;
    
    // Update active buttons in sidebar
    document.querySelectorAll('.nav-btn').forEach(btn => btn.classList.remove('active'));
    document.getElementById(`tab-btn-${tabName}`).classList.add('active');

    // Update panel visibility
    document.querySelectorAll('.tab-panel').forEach(panel => panel.classList.remove('active'));
    document.getElementById(`panel-${tabName}`).classList.add('active');

    // Update titles
    const titleEl = document.getElementById('current-panel-title');
    const descEl = document.getElementById('current-panel-desc');

    if (tabName === 'seller') {
        titleEl.textContent = 'Seller Service Control Panel';
        descEl.textContent = 'Manage Customer Orders, local stock checks, and supply replenishment.';
        loadSellerData();
    } else if (tabName === 'distributor') {
        titleEl.textContent = 'Distributor Service Control Panel';
        descEl.textContent = 'Monitor distributor inventory, process seller availability requests, and manage shipments.';
        loadDistributorData();
    } else if (tabName === 'manufacturer') {
        titleEl.textContent = 'Manufacturer Service Control Panel';
        descEl.textContent = 'Overview of production capacity, materials, finished goods stock, and transfer requests.';
        loadManufacturerData();
    }
}

// Refresh all lookup data
async function refreshAllData() {
    try {
        // Try fetching blanket models from Manufacturer API
        const modelResponse = await fetch(`${MANUFACTURER_API}/BlanketModels`);
        if (!modelResponse.ok) throw new Error('Cannot reach Manufacturer Service (Port 5030)');
        blanketModels = await modelResponse.json();

        // Fetch customers from Seller API
        const custResponse = await fetch(`${SELLER_API}/Customers`);
        if (!custResponse.ok) throw new Error('Cannot reach Seller Service (Port 5010)');
        customers = await custResponse.json();

        // Fetch sellers from Seller API
        const sellerResponse = await fetch(`${SELLER_API}/Sellers`);
        if (!sellerResponse.ok) throw new Error('Cannot reach Seller Service (Port 5010)');
        sellers = await sellerResponse.json();

        // Fetch distributors from Distributor API
        const distResponse = await fetch(`${DISTRIBUTOR_API}/Distributors`);
        if (!distResponse.ok) throw new Error('Cannot reach Distributor Service (Port 5020)');
        distributors = await distResponse.json();

        // Populate dropdown selectors
        populateDropdowns();
        
        // Load initial tab data
        switchTab(currentTab);
        
        showToast('Sync Successful', 'Successfully synchronized data with all three services.', 'fa-cloud-arrow-down', 'success');
    } catch (err) {
        console.error(err);
        showToast('Connection Offline', `Failed to sync: ${err.message}. Ensure backend is running!`, 'fa-circle-xmark', 'danger');
    }
}

// Populate selectors
function populateDropdowns() {
    // Seller Selector
    const sellerSelect = document.getElementById('seller-select');
    sellerSelect.innerHTML = sellers.map(s => `<option value="${s.id}">${s.sellerName}</option>`).join('');
    if (sellers.length > 0 && !currentSellerId) currentSellerId = sellers[0].id;
    if (currentSellerId) sellerSelect.value = currentSellerId;

    // Distributor Selector
    const distSelect = document.getElementById('distributor-select');
    distSelect.innerHTML = distributors.map(d => `<option value="${d.id}">${d.distributorName}</option>`).join('');
    if (distributors.length > 0 && !currentDistributorId) currentDistributorId = distributors[0].id;
    if (currentDistributorId) distSelect.value = currentDistributorId;

    // Form: Customer Selector
    const orderCust = document.getElementById('order-customer');
    orderCust.innerHTML = customers.map(c => `<option value="${c.id}">${c.customerName}</option>`).join('');

    // Form: Blanket Model Selector
    const orderBlanket = document.getElementById('order-blanket');
    orderBlanket.innerHTML = blanketModels.map(m => `<option value="${m.id}">${m.sku} - ${m.modelName}</option>`).join('');

    onOrderBlanketChange();
}

// Handle blanket selection change to display correct unit price
function onOrderBlanketChange() {
    const selectedModelId = parseInt(document.getElementById('order-blanket').value);
    const model = blanketModels.find(m => m.id === selectedModelId);
    if (model) {
        document.getElementById('order-price').value = model.unitPrice.toFixed(2);
    }
}

// ==========================================
// SELLER SERVICE ACTIONS
// ==========================================

async function loadSellerData() {
    currentSellerId = parseInt(document.getElementById('seller-select').value);
    if (!currentSellerId) return;

    try {
        // Fetch Seller Inventory from Seller Service
        const resInv = await fetch(`${SELLER_API}/SellerInventories`);
        const allInv = await resInv.json();
        const sellerInv = allInv.filter(i => i.sellerId === currentSellerId);
        
        // Render Inventory Table
        const tableBody = document.getElementById('seller-inventory-table');
        if (sellerInv.length === 0) {
            tableBody.innerHTML = `<tr><td colspan="6" class="text-center">No inventory items.</td></tr>`;
        } else {
            tableBody.innerHTML = sellerInv.map(inv => {
                const model = blanketModels.find(m => m.id === inv.blanketModelId) || { sku: 'N/A', modelName: 'Unknown' };
                const isLow = inv.quantityOnHand <= 5;
                const statusBadge = isLow 
                    ? `<span class="badge badge-warning">Low Stock</span>` 
                    : `<span class="badge badge-success">In Stock</span>`;
                return `
                    <tr>
                        <td>${model.sku}</td>
                        <td><strong>${model.modelName}</strong></td>
                        <td class="${isLow ? 'text-warning' : ''}">${inv.quantityOnHand}</td>
                        <td>${inv.reservedQuantity}</td>
                        <td><strong>${inv.quantityOnHand - inv.reservedQuantity}</strong></td>
                        <td>${statusBadge}</td>
                    </tr>
                `;
            }).join('');
        }

        // Fetch Customer Orders from Seller Service
        const resOrders = await fetch(`${SELLER_API}/CustomerOrders`);
        const allOrders = await resOrders.json();
        const sellerOrders = allOrders.filter(o => o.sellerId === currentSellerId).reverse();

        // Render Orders Log
        const ordersLog = document.getElementById('seller-orders-log');
        if (sellerOrders.length === 0) {
            ordersLog.innerHTML = `<div class="text-muted text-center py-4">No customer orders recorded.</div>`;
        } else {
            ordersLog.innerHTML = sellerOrders.map(o => {
                const cust = customers.find(c => c.id === o.customerId) || { customerName: 'Unknown' };
                let statusClass = 'badge-warning';
                if (o.status === 'Delivered') statusClass = 'badge-success';
                
                return `
                    <div class="log-item">
                        <div class="log-header">
                            <span class="log-title">Order #${o.orderNumber}</span>
                            <span class="badge ${statusClass}">${o.status}</span>
                        </div>
                        <div class="log-body">
                            <p><strong>Customer:</strong> ${cust.customerName}</p>
                            <p><strong>Total Amount:</strong> $${o.totalAmount.toFixed(2)}</p>
                            <p><strong>Source:</strong> ${o.finalSource || 'Pending Check'}</p>
                            <p><strong>Order Date:</strong> ${new Date(o.orderDate).toLocaleString()}</p>
                            ${o.remarks ? `<p><em>"${o.remarks}"</em></p>` : ''}
                        </div>
                    </div>
                `;
            }).join('');
        }

        // Fetch Availability Requests and Transfer Orders
        const resRequests = await fetch(`${DISTRIBUTOR_API}/AvailabilityRequests`);
        const allRequests = await resRequests.json();
        const sellerRequests = allRequests.filter(r => r.sellerId === currentSellerId).reverse();

        const resTransfers = await fetch(`${DISTRIBUTOR_API}/TransferOrders`);
        const allTransfers = await resTransfers.json();
        const sellerTransfers = allTransfers.filter(t => t.fromSellerId === currentSellerId || t.toSellerId === currentSellerId).reverse();

        // Render Stock Requests & Transfers Log
        const transfersLog = document.getElementById('seller-transfers-log');
        let requestHtml = '';

        if (sellerRequests.length > 0) {
            requestHtml += `<div class="section-title-sm mb-2" style="font-weight:600; color:var(--accent-purple);">Availability Checks</div>`;
            requestHtml += sellerRequests.map(r => {
                const model = blanketModels.find(m => m.id === r.blanketModelId) || { modelName: 'Unknown' };
                let badgeClass = 'badge-warning';
                if (r.status === 'Available') badgeClass = 'badge-success';
                if (r.status === 'NotAvailable' || r.status === 'Rejected') badgeClass = 'badge-danger';
                if (r.status === 'ProductionPossible') badgeClass = 'badge-info';

                return `
                    <div class="log-item mb-2" style="border-left: 3px solid var(--accent-purple);">
                        <div class="log-header">
                            <span class="log-title">${r.requestNumber}</span>
                            <span class="badge ${badgeClass}">${r.status}</span>
                        </div>
                        <div class="log-body">
                            <p><strong>Model:</strong> ${model.modelName}</p>
                            <p><strong>Qty:</strong> ${r.requestedQuantity} (Level: ${r.requestLevel})</p>
                            ${r.expectedLeadTimeDays ? `<p><strong>Lead Time:</strong> ${r.expectedLeadTimeDays} days</p>` : ''}
                            <p><strong>Message:</strong> <em>${r.responseMessage || 'Pending response...'}</em></p>
                        </div>
                        ${(r.status === 'Available' || r.status === 'ProductionPossible') ? `
                            <div class="log-footer">
                                <button class="btn btn-primary btn-sm" onclick="createReplenishTransfer(${r.id})">
                                    <i class="fa-solid fa-cart-flatbed"></i> Order Transfer
                                </button>
                            </div>
                        ` : ''}
                    </div>
                `;
            }).join('');
        }

        if (sellerTransfers.length > 0) {
            requestHtml += `<div class="section-title-sm mt-3 mb-2" style="font-weight:600; color:var(--accent-cyan);">Transfer Orders</div>`;
            requestHtml += sellerTransfers.map(t => {
                let badgeClass = 'badge-warning';
                if (t.status === 'InTransit') badgeClass = 'badge-info';
                if (t.status === 'Completed') badgeClass = 'badge-success';
                
                return `
                    <div class="log-item mb-2" style="border-left: 3px solid var(--accent-cyan);">
                        <div class="log-header">
                            <span class="log-title">Transfer ${t.transferNumber}</span>
                            <span class="badge ${badgeClass}">${t.status}</span>
                        </div>
                        <div class="log-body">
                            <p><strong>Route:</strong> Distributor &rarr; Seller</p>
                            <p><strong>Req Date:</strong> ${new Date(t.requestedDate).toLocaleDateString()}</p>
                            ${t.completedDate ? `<p><strong>Delivered:</strong> ${new Date(t.completedDate).toLocaleDateString()}</p>` : ''}
                        </div>
                        ${t.status === 'InTransit' ? `
                            <div class="log-footer">
                                <button class="btn btn-success btn-sm" onclick="receiveSellerTransfer(${t.id})">
                                    <i class="fa-solid fa-circle-check"></i> Mark Received
                                </button>
                            </div>
                        ` : ''}
                    </div>
                `;
            }).join('');
        }

        if (requestHtml === '') {
            transfersLog.innerHTML = `<div class="text-muted text-center py-4">No active requests or transfers.</div>`;
        } else {
            transfersLog.innerHTML = requestHtml;
        }

    } catch (err) {
        console.error(err);
        showToast('Error', 'Failed to retrieve Seller Service datasets.', 'fa-circle-xmark', 'danger');
    }
}

// Receive order and check availability
async function handlePlaceOrder(event) {
    event.preventDefault();
    const customerId = parseInt(document.getElementById('order-customer').value);
    const blanketModelId = parseInt(document.getElementById('order-blanket').value);
    const quantity = parseInt(document.getElementById('order-qty').value);
    const remarks = document.getElementById('order-remarks').value;

    const model = blanketModels.find(m => m.id === blanketModelId);
    const totalAmount = model.unitPrice * quantity;
    const orderNumber = 'ORD-' + Math.floor(100000 + Math.random() * 900000);

    try {
        // Fetch inventory to check stock
        const resInv = await fetch(`${SELLER_API}/SellerInventories`);
        const allInv = await resInv.json();
        const itemInv = allInv.find(i => i.sellerId === currentSellerId && i.blanketModelId === blanketModelId);

        if (itemInv && (itemInv.quantityOnHand - itemInv.reservedQuantity) >= quantity) {
            // STOCK AVAILABLE - Process order immediately
            showToast('Stock Available', 'Fulfilling customer order from local stock...', 'fa-circle-check', 'success');

            // 1. Create order as Delivered
            const newOrder = {
                orderNumber,
                customerId,
                sellerId: currentSellerId,
                status: 'Delivered',
                finalSource: 'SellerStock',
                expectedDeliveryDate: new Date(),
                totalAmount,
                remarks
            };
            const orderRes = await fetch(`${SELLER_API}/CustomerOrders`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(newOrder)
            });
            const orderData = await orderRes.json();

            // 2. Decrement Seller Inventory
            itemInv.quantityOnHand -= quantity;
            await fetch(`${SELLER_API}/SellerInventories/${itemInv.id}`, {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(itemInv)
            });

            // 3. Log Stock Movement
            const movement = {
                blanketModelId,
                sellerInventoryId: itemInv.id,
                factoryInventoryId: null,
                distributorInventoryId: null,
                movementType: 'Issue',
                quantity: quantity,
                customerOrderId: orderData.id,
                createdAt: new Date()
            };
            await fetch(`${SELLER_API}/StockMovements`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(movement)
            });

            showToast('Order Fulfilling', `Order #${orderNumber} processed successfully.`, 'fa-circle-check', 'success');
            document.getElementById('customer-order-form').reset();
            populateDropdowns();
            loadSellerData();
        } else {
            // STOCK NOT AVAILABLE - Trigger Availability request chain
            showToast('Stock Unavailable', 'Seller stock insufficient. Creating Pending order & requesting Distributor stock...', 'fa-clock', 'warning');

            // 1. Create Pending Order
            const pendingOrder = {
                orderNumber,
                customerId,
                sellerId: currentSellerId,
                status: 'Pending',
                finalSource: 'DistributorStock',
                expectedDeliveryDate: new Date(Date.now() + 7 * 24 * 60 * 60 * 1000),
                totalAmount,
                remarks
            };
            const orderRes = await fetch(`${SELLER_API}/CustomerOrders`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(pendingOrder)
            });
            const orderData = await orderRes.json();

            // 2. Automatically create Availability Request to Distributor (Port 5020)
            const seller = sellers.find(s => s.id === currentSellerId);
            const reqNumber = 'REQ-S2D-' + Math.floor(100000 + Math.random() * 900000);
            const req = {
                requestNumber: reqNumber,
                customerOrderId: orderData.id,
                blanketModelId,
                sellerId: currentSellerId,
                distributorId: seller.distributorId,
                requestedQuantity: quantity,
                requestLevel: 'SellerToDistributor',
                status: 'Pending',
                responseMessage: 'Seller out of stock. Contacting assigned distributor.'
            };

            await fetch(`${DISTRIBUTOR_API}/AvailabilityRequests`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(req)
            });

            showToast('Request Forwarded', `Availability request ${reqNumber} submitted to Distributor.`, 'fa-paper-plane');
            document.getElementById('customer-order-form').reset();
            populateDropdowns();
            loadSellerData();
        }

    } catch (err) {
        console.error(err);
        showToast('Order Error', `Failed to place customer order: ${err.message}`, 'fa-circle-xmark', 'danger');
    }
}

// Request Transfer based on Available request
async function createReplenishTransfer(reqId) {
    try {
        const resReq = await fetch(`${DISTRIBUTOR_API}/AvailabilityRequests/${reqId}`);
        const req = await resReq.json();

        // Create a Transfer Order from Distributor -> Seller
        const transNum = 'TRN-D2S-' + Math.floor(100000 + Math.random() * 900000);
        const trans = {
            transferNumber: transNum,
            customerOrderId: req.customerOrderId,
            fromLocationType: 'Distributor',
            toLocationType: 'Seller',
            fromDistributorId: req.distributorId,
            toSellerId: req.sellerId,
            status: 'Pending',
            requestedDate: new Date(),
            remarks: `Replenishment for customer order via Availability Request ${req.requestNumber}`
        };

        // Submit to Distributor Service
        const transRes = await fetch(`${DISTRIBUTOR_API}/TransferOrders`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(trans)
        });
        const transData = await transRes.json();

        // Create Transfer Order Item
        const transItem = {
            transferOrderId: transData.id,
            blanketModelId: req.blanketModelId,
            quantity: req.requestedQuantity
        };
        await fetch(`${DISTRIBUTOR_API}/TransferOrderItems`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(transItem)
        });

        // Update Availability Request to Available/Completed state
        req.status = 'Available';
        req.responseMessage = 'Replenishment transfer order created.';
        req.responseDate = new Date();
        await fetch(`${DISTRIBUTOR_API}/AvailabilityRequests/${req.id}`, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(req)
        });

        showToast('Transfer Requested', `Transfer Order ${transNum} submitted to Distributor.`, 'fa-truck-arrow-right', 'success');
        loadSellerData();
    } catch (err) {
        console.error(err);
        showToast('Error', 'Failed to create replenishment transfer order.', 'fa-circle-xmark', 'danger');
    }
}

// Receive Shipment
async function receiveSellerTransfer(transId) {
    try {
        const transRes = await fetch(`${DISTRIBUTOR_API}/TransferOrders/${transId}`);
        const trans = await transRes.json();

        // Get transfer items
        const itemsRes = await fetch(`${DISTRIBUTOR_API}/TransferOrderItems`);
        const allItems = await itemsRes.json();
        const transItem = allItems.find(i => i.transferOrderId === trans.id);

        if (!transItem) throw new Error('Transfer details missing.');

        // Update Seller Inventory
        const sellerInvRes = await fetch(`${SELLER_API}/SellerInventories`);
        const allSellerInv = await sellerInvRes.json();
        let sellerInv = allSellerInv.find(i => i.sellerId === trans.toSellerId && i.blanketModelId === transItem.blanketModelId);

        if (sellerInv) {
            sellerInv.quantityOnHand += transItem.quantity;
            await fetch(`${SELLER_API}/SellerInventories/${sellerInv.id}`, {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(sellerInv)
            });
        } else {
            // Create new inventory
            const newInv = {
                sellerId: trans.toSellerId,
                blanketModelId: transItem.blanketModelId,
                quantityOnHand: transItem.quantity,
                reservedQuantity: 0,
                lastUpdated: new Date()
            };
            const postRes = await fetch(`${SELLER_API}/SellerInventories`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(newInv)
            });
            sellerInv = await postRes.json();
        }

        // Log Stock Movement
        const movement = {
            blanketModelId: transItem.blanketModelId,
            sellerInventoryId: sellerInv.id,
            factoryInventoryId: null,
            distributorInventoryId: null,
            movementType: 'Receive',
            quantity: transItem.quantity,
            transferOrderId: trans.id,
            createdAt: new Date()
        };
        await fetch(`${SELLER_API}/StockMovements`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(movement)
        });

        // Update Transfer Order Status
        trans.status = 'Completed';
        trans.completedDate = new Date();
        await fetch(`${DISTRIBUTOR_API}/TransferOrders/${trans.id}`, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(trans)
        });

        // Fulfill the Customer Order
        if (trans.customerOrderId) {
            const orderRes = await fetch(`${SELLER_API}/CustomerOrders/${trans.customerOrderId}`);
            const customerOrder = await orderRes.json();

            customerOrder.status = 'Delivered';
            customerOrder.finalSource = 'DistributorStock';
            await fetch(`${SELLER_API}/CustomerOrders/${customerOrder.id}`, {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(customerOrder)
            });

            // Adjust inventory for the fulfilled customer order (sales deduction)
            const reloadedSellerInvRes = await fetch(`${SELLER_API}/SellerInventories`);
            const reloadedSellerInv = await reloadedSellerInvRes.json();
            sellerInv = reloadedSellerInv.find(i => i.sellerId === trans.toSellerId && i.blanketModelId === transItem.blanketModelId);
            
            sellerInv.quantityOnHand -= transItem.quantity;
            await fetch(`${SELLER_API}/SellerInventories/${sellerInv.id}`, {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(sellerInv)
            });

            // Log sales deduction movement (Issue)
            const salesMovement = {
                blanketModelId: transItem.blanketModelId,
                sellerInventoryId: sellerInv.id,
                factoryInventoryId: null,
                distributorInventoryId: null,
                movementType: 'Issue',
                quantity: transItem.quantity,
                customerOrderId: customerOrder.id,
                createdAt: new Date()
            };
            await fetch(`${SELLER_API}/StockMovements`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(salesMovement)
            });
        }

        showToast('Stock Received', 'Stock transferred and Customer Order fully fulfilled.', 'fa-circle-check', 'success');
        loadSellerData();
    } catch (err) {
        console.error(err);
        showToast('Error', `Fulfillment failed: ${err.message}`, 'fa-circle-xmark', 'danger');
    }
}

// ==========================================
// DISTRIBUTOR SERVICE ACTIONS
// ==========================================

async function loadDistributorData() {
    currentDistributorId = parseInt(document.getElementById('distributor-select').value);
    if (!currentDistributorId) return;

    try {
        // Fetch Distributor Inventory
        const resInv = await fetch(`${DISTRIBUTOR_API}/DistributorInventories`);
        const allInv = await resInv.json();
        const distInv = allInv.filter(i => i.distributorId === currentDistributorId);

        // Render Inventory Table
        const tableBody = document.getElementById('distributor-inventory-table');
        if (distInv.length === 0) {
            tableBody.innerHTML = `<tr><td colspan="6" class="text-center">No inventory items.</td></tr>`;
        } else {
            tableBody.innerHTML = distInv.map(inv => {
                const model = blanketModels.find(m => m.id === inv.blanketModelId) || { sku: 'N/A', modelName: 'Unknown' };
                const isLow = inv.quantityOnHand <= 10;
                const statusBadge = isLow 
                    ? `<span class="badge badge-warning">Low Stock</span>` 
                    : `<span class="badge badge-success">In Stock</span>`;
                return `
                    <tr>
                        <td>${model.sku}</td>
                        <td><strong>${model.modelName}</strong></td>
                        <td class="${isLow ? 'text-warning' : ''}">${inv.quantityOnHand}</td>
                        <td>${inv.reservedQuantity}</td>
                        <td><strong>${inv.quantityOnHand - inv.reservedQuantity}</strong></td>
                        <td>${statusBadge}</td>
                    </tr>
                `;
            }).join('');
        }

        // Fetch Availability Requests for this Distributor
        const resReq = await fetch(`${DISTRIBUTOR_API}/AvailabilityRequests`);
        const allReq = await resReq.json();
        const distReq = allReq.filter(r => r.distributorId === currentDistributorId).reverse();

        // Render Incoming Requests
        const reqTable = document.getElementById('distributor-requests-table');
        if (distReq.length === 0) {
            reqTable.innerHTML = `<tr><td colspan="7" class="text-center">No active stock requests.</td></tr>`;
        } else {
            reqTable.innerHTML = distReq.map(r => {
                const seller = sellers.find(s => s.id === r.sellerId) || { sellerName: 'Unknown' };
                const model = blanketModels.find(m => m.id === r.blanketModelId) || { modelName: 'Unknown' };
                
                let badgeClass = 'badge-warning';
                if (r.status === 'Available') badgeClass = 'badge-success';
                if (r.status === 'NotAvailable' || r.status === 'Rejected') badgeClass = 'badge-danger';
                if (r.status === 'ProductionPossible') badgeClass = 'badge-info';

                let actionButton = '';
                if (r.status === 'Pending') {
                    actionButton = `
                        <button class="btn btn-primary btn-sm" onclick="checkDistributorStock(${r.id})">
                            <i class="fa-solid fa-magnifying-glass"></i> Check Stock
                        </button>
                    `;
                } else if (r.status === 'ProductionPossible') {
                    // Forward response to Seller
                    actionButton = `
                        <button class="btn btn-secondary btn-sm" onclick="forwardManufacturerResponse(${r.id})">
                            <i class="fa-solid fa-share"></i> Forward to Seller
                        </button>
                    `;
                }

                return `
                    <tr>
                        <td><strong>${r.requestNumber}</strong></td>
                        <td>${seller.sellerName}</td>
                        <td>${model.modelName}</td>
                        <td>${r.requestedQuantity}</td>
                        <td><small>${r.requestLevel}</small></td>
                        <td><span class="badge ${badgeClass}">${r.status}</span></td>
                        <td>${actionButton}</td>
                    </tr>
                `;
            }).join('');
        }

        // Fetch Outbound Shipments / Transfer Orders
        const resTransfers = await fetch(`${DISTRIBUTOR_API}/TransferOrders`);
        const allTrans = await resTransfers.json();
        const distTrans = allTrans.filter(t => t.fromDistributorId === currentDistributorId).reverse();

        // Render Transfers
        const transfersLog = document.getElementById('distributor-transfers-log');
        if (distTrans.length === 0) {
            transfersLog.innerHTML = `<div class="text-muted text-center py-4">No outbound shipments scheduled.</div>`;
        } else {
            transfersLog.innerHTML = distTrans.map(t => {
                const seller = sellers.find(s => s.id === t.toSellerId) || { sellerName: 'Unknown' };
                let badgeClass = 'badge-warning';
                if (t.status === 'InTransit') badgeClass = 'badge-info';
                if (t.status === 'Completed') badgeClass = 'badge-success';

                return `
                    <div class="log-item">
                        <div class="log-header">
                            <span class="log-title">${t.transferNumber}</span>
                            <span class="badge ${badgeClass}">${t.status}</span>
                        </div>
                        <div class="log-body">
                            <p><strong>To Seller:</strong> ${seller.sellerName}</p>
                            <p><strong>Date:</strong> ${new Date(t.requestedDate).toLocaleDateString()}</p>
                            ${t.remarks ? `<p><em>"${t.remarks}"</em></p>` : ''}
                        </div>
                        ${t.status === 'Pending' ? `
                            <div class="log-footer">
                                <button class="btn btn-primary btn-sm" onclick="shipDistributorTransfer(${t.id})">
                                    <i class="fa-solid fa-truck-fast"></i> Approve & Ship
                                </button>
                            </div>
                        ` : ''}
                    </div>
                `;
            }).join('');
        }

    } catch (err) {
        console.error(err);
        showToast('Error', 'Failed to retrieve Distributor Service datasets.', 'fa-circle-xmark', 'danger');
    }
}

// Action: Check Distributor Stock, forward to Manufacturer if not available
async function checkDistributorStock(reqId) {
    try {
        const resReq = await fetch(`${DISTRIBUTOR_API}/AvailabilityRequests/${reqId}`);
        const req = await resReq.json();

        // Check stock
        const resInv = await fetch(`${DISTRIBUTOR_API}/DistributorInventories`);
        const allInv = await resInv.json();
        const distInv = allInv.find(i => i.distributorId === currentDistributorId && i.blanketModelId === req.blanketModelId);

        if (distInv && (distInv.quantityOnHand - distInv.reservedQuantity) >= req.requestedQuantity) {
            // Stock is available at Distributor!
            req.status = 'Available';
            req.responseMessage = 'Stock available at distributor. Lead time 1 day.';
            req.responseDate = new Date();
            req.expectedLeadTimeDays = 1;

            await fetch(`${DISTRIBUTOR_API}/AvailabilityRequests/${req.id}`, {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(req)
            });

            showToast('Stock Available', 'Stock is available in distributor inventory.', 'fa-circle-check', 'success');
            loadDistributorData();
        } else {
            // Stock NOT available at Distributor! Forward request to Manufacturer (Port 5030)
            showToast('Stock Unavailable', 'Distributor stock insufficient. Forwarding request to Manufacturer...', 'fa-clock', 'warning');

            // 1. Update current request message, but keep status Pending
            req.status = 'Pending';
            req.responseMessage = 'Distributor out of stock. Contacting Cozy Comfort Factory.';
            await fetch(`${DISTRIBUTOR_API}/AvailabilityRequests/${req.id}`, {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(req)
            });

            // 2. Submit new Availability Request to Manufacturer Service (Port 5030)
            const reqNumber = 'REQ-D2M-' + Math.floor(100000 + Math.random() * 900000);
            const factoryReq = {
                requestNumber: reqNumber,
                customerOrderId: req.customerOrderId, // link it back to original order
                blanketModelId: req.blanketModelId,
                sellerId: req.sellerId,
                distributorId: currentDistributorId,
                requestedQuantity: req.requestedQuantity,
                requestLevel: 'DistributorToFactory',
                status: 'Pending',
                responseMessage: `Distributor out of stock. Checking production capacity at factory.`
            };

            await fetch(`${MANUFACTURER_API}/AvailabilityRequests`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(factoryReq)
            });

            showToast('Forwarded to Factory', `Submitted request ${reqNumber} to Manufacturer.`, 'fa-paper-plane');
            loadDistributorData();
        }
    } catch (err) {
        console.error(err);
        showToast('Error', 'Failed to perform distributor stock check.', 'fa-circle-xmark', 'danger');
    }
}

// Forward Response to Seller
async function forwardManufacturerResponse(reqId) {
    try {
        const resReq = await fetch(`${DISTRIBUTOR_API}/AvailabilityRequests/${reqId}`);
        const distReq = await resReq.json();

        // Get the manufacturer request linked to this customer order
        const resMft = await fetch(`${MANUFACTURER_API}/AvailabilityRequests`);
        const allMft = await resMft.json();
        const mftReq = allMft.find(r => r.customerOrderId === distReq.customerOrderId && r.requestLevel === 'DistributorToFactory');

        if (mftReq && mftReq.status === 'ProductionPossible') {
            distReq.status = 'ProductionPossible';
            distReq.expectedLeadTimeDays = mftReq.expectedLeadTimeDays;
            distReq.responseMessage = `Manufacturer confirmed: "${mftReq.responseMessage}". Total lead time ${mftReq.expectedLeadTimeDays + 1} days.`;
            distReq.responseDate = new Date();

            await fetch(`${DISTRIBUTOR_API}/AvailabilityRequests/${distReq.id}`, {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(distReq)
            });

            // Also place an automatically approved factory to distributor replenishment transfer
            const transNum = 'TRN-M2D-' + Math.floor(100000 + Math.random() * 900000);
            const trans = {
                transferNumber: transNum,
                customerOrderId: distReq.customerOrderId,
                fromLocationType: 'Factory',
                toLocationType: 'Distributor',
                fromDistributorId: currentDistributorId,
                status: 'Pending',
                requestedDate: new Date(),
                remarks: `Stock replenishing from factory for seller order.`
            };

            const transRes = await fetch(`${MANUFACTURER_API}/TransferOrders`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(trans)
            });
            const transData = await transRes.json();

            const transItem = {
                transferOrderId: transData.id,
                blanketModelId: distReq.blanketModelId,
                quantity: distReq.requestedQuantity
            };
            await fetch(`${MANUFACTURER_API}/TransferOrderItems`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(transItem)
            });

            showToast('Response Forwarded', 'Manufacturer lead times sent to Seller, replenishment transfer created.', 'fa-circle-check', 'success');
            loadDistributorData();
        } else {
            showToast('No Response yet', 'Waiting for Manufacturer to response to the pending request.', 'fa-clock', 'warning');
        }
    } catch (err) {
        console.error(err);
        showToast('Error', 'Failed to forward manufacturer response.', 'fa-circle-xmark', 'danger');
    }
}

// Approve Outbound Shipment to Seller
async function shipDistributorTransfer(transId) {
    try {
        const transRes = await fetch(`${DISTRIBUTOR_API}/TransferOrders/${transId}`);
        const trans = await transRes.json();

        // Get transfer items
        const itemsRes = await fetch(`${DISTRIBUTOR_API}/TransferOrderItems`);
        const allItems = await itemsRes.json();
        const transItem = allItems.find(i => i.transferOrderId === trans.id);

        if (!transItem) throw new Error('Transfer item details missing.');

        // Verify Distributor Stock
        const resInv = await fetch(`${DISTRIBUTOR_API}/DistributorInventories`);
        const allInv = await resInv.json();
        const distInv = allInv.find(i => i.distributorId === currentDistributorId && i.blanketModelId === transItem.blanketModelId);

        if (!distInv || distInv.quantityOnHand < transItem.quantity) {
            showToast('Insufficient Stock', 'Distributor does not have enough physical stock. Fulfill manufacturer replenishment first.', 'fa-circle-xmark', 'danger');
            return;
        }

        // Deduct Distributor Stock
        distInv.quantityOnHand -= transItem.quantity;
        await fetch(`${DISTRIBUTOR_API}/DistributorInventories/${distInv.id}`, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(distInv)
        });

        // Log Stock Movement (Issue)
        const movement = {
            blanketModelId: transItem.blanketModelId,
            distributorInventoryId: distInv.id,
            factoryInventoryId: null,
            sellerInventoryId: null,
            movementType: 'Issue',
            quantity: transItem.quantity,
            transferOrderId: trans.id,
            createdAt: new Date()
        };
        await fetch(`${DISTRIBUTOR_API}/StockMovements`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(movement)
        });

        // Update Transfer Order Status to InTransit (Shipped)
        trans.status = 'InTransit';
        trans.approvedDate = new Date();
        await fetch(`${DISTRIBUTOR_API}/TransferOrders/${trans.id}`, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(trans)
        });

        showToast('Shipment Dispatched', `Transfer Order ${trans.transferNumber} marked as InTransit.`, 'fa-truck-fast', 'success');
        loadDistributorData();
    } catch (err) {
        console.error(err);
        showToast('Error', `Shipment dispatch failed: ${err.message}`, 'fa-circle-xmark', 'danger');
    }
}

// ==========================================
// MANUFACTURER SERVICE ACTIONS
// ==========================================

async function loadManufacturerData() {
    try {
        // Fetch Factory Inventory
        const resInv = await fetch(`${MANUFACTURER_API}/FactoryInventories`);
        const factoryInv = await resInv.json();

        // Render Factory Inventory
        const tableBody = document.getElementById('manufacturer-inventory-table');
        if (factoryInv.length === 0) {
            tableBody.innerHTML = `<tr><td colspan="5" class="text-center">No inventory records.</td></tr>`;
        } else {
            tableBody.innerHTML = factoryInv.map(inv => {
                const model = blanketModels.find(m => m.id === inv.blanketModelId) || { sku: 'N/A', modelName: 'Unknown' };
                return `
                    <tr>
                        <td>${model.sku}</td>
                        <td><strong>${model.modelName}</strong></td>
                        <td>${inv.quantityOnHand}</td>
                        <td>${inv.reservedQuantity}</td>
                        <td><strong>${inv.quantityOnHand - inv.reservedQuantity}</strong></td>
                    </tr>
                `;
            }).join('');
        }

        // Fetch Production Capacities
        const resCap = await fetch(`${MANUFACTURER_API}/ProductionCapacities`);
        const capacities = await resCap.json();

        // Render Capacities
        const capBody = document.getElementById('manufacturer-capacity-table');
        if (capacities.length === 0) {
            capBody.innerHTML = `<tr><td colspan="5" class="text-center">No capacity settings defined.</td></tr>`;
        } else {
            capBody.innerHTML = capacities.map(c => {
                const model = blanketModels.find(m => m.id === c.blanketModelId) || { modelName: 'Unknown' };
                return `
                    <tr>
                        <td><strong>${model.modelName}</strong></td>
                        <td>${c.dailyCapacity} blankets</td>
                        <td>${c.weeklyCapacity} blankets</td>
                        <td>${c.currentPendingQuantity} requested</td>
                        <td><span class="badge badge-info">${c.leadTimeDays} Days</span></td>
                    </tr>
                `;
            }).join('');
        }

        // Fetch Availability Requests from Distributors
        const resReq = await fetch(`${MANUFACTURER_API}/AvailabilityRequests`);
        const allReq = await resReq.json();
        const mftReq = allReq.filter(r => r.requestLevel === 'DistributorToFactory').reverse();

        // Fetch Outbound Transfers (to Distributors)
        const resTrans = await fetch(`${MANUFACTURER_API}/TransferOrders`);
        const allTrans = await resTrans.json();
        const mftTrans = allTrans.filter(t => t.fromLocationType === 'Factory').reverse();

        // Render Production/Transfer Logs
        const requestsLog = document.getElementById('manufacturer-requests-log');
        let mftLogHtml = '';

        if (mftReq.length > 0) {
            mftLogHtml += `<div class="section-title-sm mb-2" style="font-weight:600; color:var(--accent-purple);">Capacity Requests</div>`;
            mftLogHtml += mftReq.map(r => {
                const dist = distributors.find(d => d.id === r.distributorId) || { distributorName: 'Unknown' };
                const model = blanketModels.find(m => m.id === r.blanketModelId) || { modelName: 'Unknown' };
                let badgeClass = 'badge-warning';
                if (r.status === 'ProductionPossible') badgeClass = 'badge-success';

                return `
                    <div class="log-item mb-2" style="border-left: 3px solid var(--accent-purple);">
                        <div class="log-header">
                            <span class="log-title">${r.requestNumber}</span>
                            <span class="badge ${badgeClass}">${r.status}</span>
                        </div>
                        <div class="log-body">
                            <p><strong>From Distributor:</strong> ${dist.distributorName}</p>
                            <p><strong>Model:</strong> ${model.modelName}</p>
                            <p><strong>Quantity:</strong> ${r.requestedQuantity}</p>
                            <p><strong>Status:</strong> ${r.responseMessage}</p>
                        </div>
                        ${r.status === 'Pending' ? `
                            <div class="log-footer">
                                <button class="btn btn-primary btn-sm" onclick="confirmFactoryCapacity(${r.id})">
                                    <i class="fa-solid fa-circle-check"></i> Confirm Capacity & Lead Time
                                </button>
                            </div>
                        ` : ''}
                    </div>
                `;
            }).join('');
        }

        if (mftTrans.length > 0) {
            mftLogHtml += `<div class="section-title-sm mt-3 mb-2" style="font-weight:600; color:var(--accent-cyan);"> replenishments to Distributors</div>`;
            mftLogHtml += mftTrans.map(t => {
                const dist = distributors.find(d => d.id === t.fromDistributorId) || { distributorName: 'Unknown' };
                let badgeClass = 'badge-warning';
                if (t.status === 'InTransit') badgeClass = 'badge-info';
                if (t.status === 'Completed') badgeClass = 'badge-success';

                return `
                    <div class="log-item mb-2" style="border-left: 3px solid var(--accent-cyan);">
                        <div class="log-header">
                            <span class="log-title">${t.transferNumber}</span>
                            <span class="badge ${badgeClass}">${t.status}</span>
                        </div>
                        <div class="log-body">
                            <p><strong>To Distributor:</strong> ${dist.distributorName}</p>
                            <p><strong>Req Date:</strong> ${new Date(t.requestedDate).toLocaleDateString()}</p>
                        </div>
                        ${t.status === 'Pending' ? `
                            <div class="log-footer">
                                <button class="btn btn-primary btn-sm" onclick="shipFactoryTransfer(${t.id})">
                                    <i class="fa-solid fa-truck-ramp-box"></i> Ship replenishment
                                </button>
                            </div>
                        ` : ''}
                    </div>
                `;
            }).join('');
        }

        if (mftLogHtml === '') {
            requestsLog.innerHTML = `<div class="text-muted text-center py-4">No active factory requests.</div>`;
        } else {
            requestsLog.innerHTML = mftLogHtml;
        }

    } catch (err) {
        console.error(err);
        showToast('Error', 'Failed to retrieve Manufacturer Service datasets.', 'fa-circle-xmark', 'danger');
    }
}

// Action: Confirm factory capacity and calculate lead time
async function confirmFactoryCapacity(reqId) {
    try {
        const resReq = await fetch(`${MANUFACTURER_API}/AvailabilityRequests/${reqId}`);
        const req = await resReq.json();

        // Fetch production capacity for the model
        const resCap = await fetch(`${MANUFACTURER_API}/ProductionCapacities`);
        const capacities = await resCap.json();
        const modelCap = capacities.find(c => c.blanketModelId === req.blanketModelId);
        
        const leadTime = modelCap ? modelCap.leadTimeDays : 3;

        // Update the request status to ProductionPossible
        req.status = 'ProductionPossible';
        req.expectedLeadTimeDays = leadTime;
        req.responseMessage = `Factory production capacity confirmed. Production will take ${leadTime} days.`;
        req.responseDate = new Date();

        await fetch(`${MANUFACTURER_API}/AvailabilityRequests/${req.id}`, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(req)
        });

        // Update current pending backlog in production capacities
        if (modelCap) {
            modelCap.currentPendingQuantity += req.requestedQuantity;
            await fetch(`${MANUFACTURER_API}/ProductionCapacities/${modelCap.id}`, {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(modelCap)
            });
        }

        showToast('Capacity Confirmed', `Lead time calculated: ${leadTime} days. Response sent.`, 'fa-circle-check', 'success');
        loadManufacturerData();
    } catch (err) {
        console.error(err);
        showToast('Error', 'Failed to confirm factory capacity.', 'fa-circle-xmark', 'danger');
    }
}

// Action: Ship replenishment stock from Manufacturer to Distributor
async function shipFactoryTransfer(transId) {
    try {
        const transRes = await fetch(`${MANUFACTURER_API}/TransferOrders/${transId}`);
        const trans = await transRes.json();

        // Get transfer items
        const itemsRes = await fetch(`${MANUFACTURER_API}/TransferOrderItems`);
        const allItems = await itemsRes.json();
        const transItem = allItems.find(i => i.transferOrderId === trans.id);

        if (!transItem) throw new Error('Transfer item details missing.');

        // Get Factory Inventory
        const resInv = await fetch(`${MANUFACTURER_API}/FactoryInventories`);
        const allInv = await resInv.json();
        let factoryInv = allInv.find(i => i.blanketModelId === transItem.blanketModelId);

        if (!factoryInv || factoryInv.quantityOnHand < transItem.quantity) {
            // Create stock at factory (simulate production completion!)
            showToast('Simulating Production', 'Insufficient finished goods. Simulating production batch run...', 'fa-gears', 'warning');
            if (factoryInv) {
                factoryInv.quantityOnHand += (transItem.quantity + 20); // produce a batch
                await fetch(`${MANUFACTURER_API}/FactoryInventories/${factoryInv.id}`, {
                    method: 'PUT',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify(factoryInv)
                });
            } else {
                const newFctInv = {
                    blanketModelId: transItem.blanketModelId,
                    quantityOnHand: transItem.quantity + 20,
                    reservedQuantity: 0,
                    lastUpdated: new Date()
                };
                const postRes = await fetch(`${MANUFACTURER_API}/FactoryInventories`, {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify(newFctInv)
                });
                factoryInv = await postRes.json();
            }
            
            // Re-fetch
            const reResInv = await fetch(`${MANUFACTURER_API}/FactoryInventories`);
            const reAllInv = await reResInv.json();
            factoryInv = reAllInv.find(i => i.blanketModelId === transItem.blanketModelId);
        }

        // Deduct Factory Inventory
        factoryInv.quantityOnHand -= transItem.quantity;
        await fetch(`${MANUFACTURER_API}/FactoryInventories/${factoryInv.id}`, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(factoryInv)
        });

        // Log Factory Stock Movement (Issue)
        const factoryMovement = {
            blanketModelId: transItem.blanketModelId,
            factoryInventoryId: factoryInv.id,
            distributorInventoryId: null,
            sellerInventoryId: null,
            movementType: 'Issue',
            quantity: transItem.quantity,
            transferOrderId: trans.id,
            createdAt: new Date()
        };
        await fetch(`${MANUFACTURER_API}/StockMovements`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(factoryMovement)
        });

        // Add Stock to Distributor Inventory
        const distInvRes = await fetch(`${DISTRIBUTOR_API}/DistributorInventories`);
        const allDistInv = await distInvRes.json();
        let distInv = allDistInv.find(i => i.distributorId === trans.fromDistributorId && i.blanketModelId === transItem.blanketModelId);

        if (distInv) {
            distInv.quantityOnHand += transItem.quantity;
            await fetch(`${DISTRIBUTOR_API}/DistributorInventories/${distInv.id}`, {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(distInv)
            });
        } else {
            const newDistInv = {
                distributorId: trans.fromDistributorId,
                blanketModelId: transItem.blanketModelId,
                quantityOnHand: transItem.quantity,
                reservedQuantity: 0,
                lastUpdated: new Date()
            };
            const postRes = await fetch(`${DISTRIBUTOR_API}/DistributorInventories`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(newDistInv)
            });
            distInv = await postRes.json();
        }

        // Log Distributor Stock Movement (Receive)
        const distMovement = {
            blanketModelId: transItem.blanketModelId,
            distributorInventoryId: distInv.id,
            factoryInventoryId: null,
            sellerInventoryId: null,
            movementType: 'Receive',
            quantity: transItem.quantity,
            transferOrderId: trans.id,
            createdAt: new Date()
        };
        await fetch(`${MANUFACTURER_API}/StockMovements`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(distMovement)
        });

        // Update Transfer Order Status to Completed
        trans.status = 'Completed';
        trans.completedDate = new Date();
        await fetch(`${MANUFACTURER_API}/TransferOrders/${trans.id}`, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(trans)
        });

        // Update linked capacity production backlog
        const resCap = await fetch(`${MANUFACTURER_API}/ProductionCapacities`);
        const capacities = await resCap.json();
        const modelCap = capacities.find(c => c.blanketModelId === transItem.blanketModelId);
        if (modelCap) {
            modelCap.currentPendingQuantity = Math.max(0, modelCap.currentPendingQuantity - transItem.quantity);
            await fetch(`${MANUFACTURER_API}/ProductionCapacities/${modelCap.id}`, {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(modelCap)
            });
        }

        showToast('Shipment Dispatched', `Replenishment transfer ${trans.transferNumber} sent to Distributor.`, 'fa-circle-check', 'success');
        loadManufacturerData();
    } catch (err) {
        console.error(err);
        showToast('Error', `Factory dispatch failed: ${err.message}`, 'fa-circle-xmark', 'danger');
    }
}

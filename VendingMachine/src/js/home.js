$(document).ready(function (){

updateInventory();
updateMoneyAmount();

    $('#nickelButton').click(function(event){
        money += 0.05;
        nickelsIn += 1;
        updateMoneyAmount();
    });

    $('#dimeButton').click(function(event){
        money += 0.10;
        dimesIn += 1;
        updateMoneyAmount();
    });

    $('#quarterButton').click(function(event){
        money += 0.25;
        quartersIn += 1;
        updateMoneyAmount();
    });

    $('#dollarButton').click(function(event){
        money += 1;
        dollarsIn += 1;
        updateMoneyAmount();
    });

    $('#purchaseButton').click(function(event){
        requestVend();
    });

    $('#closeModal').click(function(event){
        $('#errorModal').modal('hide');
    })

    $('#changeButton').click(function(event){
        $('#messageDisplay').val('');
        $('#changeDisplay').val('');
        money = 0.00;
        updateMoneyAmount();
       
    
        quartersIn += dollarsIn * 4;
        $('#changeDisplay').val($('#changeDisplay').val() + 'Quarters: ' + quartersIn + "\n");
        $('#changeDisplay').val($('#changeDisplay').val() + 'Dimes: ' + dimesIn + "\n");
        $('#changeDisplay').val($('#changeDisplay').val() + 'Nickels: ' + nickelsIn + "\n");
        $('#changeDisplay').val($('#changeDisplay').val() + 'Pennies: 0');
        dollarsIn = 0;
        quartersIn = 0;
        dimesIn = 0;
        nickelsIn = 0;
    });

    $(document).on('click', '.itemButton', (function(event){
        var item = event.target.id;
        $('#selectedItem').val(item.substring(4, 5));
        itemSelected = true;
    }));

});

var itemSelected = false;
var money = 0.00;
var dollarsIn = 0;
var quartersIn = 0;
var dimesIn = 0;
var nickelsIn = 0;

function updateInventory(){
    var indexer = 0;
    
    $('#products').empty();
    var productArea = $('#products');
    $.ajax({
        type: 'GET',
        url: 'http://localhost:8080/items',
        success: function(itemArray){
            $.each(itemArray, function(index, item){
                indexer++;

                var id = item.id;
                var name = item.name;
                var price = item.price;
                var quantity = item.quantity;
                var buttonType = 'btn-success';
                if(item.quantity == 0) buttonType = 'btn-secondary';

                var button = "<button type='button' " + "id='item" + id +"'" + "class='itemButton btn "
                button += buttonType;
                button += " btn-lg' style='width:155px; height:155px; margin-left:30px;'>";
                button += '<h6 style="text-align:left;">' + id + '</h6>';
                button +=  name;
                button += '<br>' + '$' + price.toFixed(2);
                button += '<br><br>' + 'Quantity Left: ' + quantity;
                button += '</button>';
                if(indexer == 3) button += '<div class="w-100"></div><br>';
                if(indexer == 3) indexer = 0;
                productArea.append(button);

            });

        },
        error: function(a, b, c){
            
        }

    });
}

function requestVend(){
    var changeField = $('#changeDisplay');
    var messageBox = $('#messageDisplay');
    var amount = $('#moneyIn').val().substring(1);
    var selectedItem = $('#selectedItem').val();
    if(!itemSelected)
    {
    noItemSelectedModal();
    
    }
    else {
        resetChange();
    $.ajax({
        type: 'GET',
        url: 'http://localhost:8080/money/' + amount + '/item/' + selectedItem,
        success: function(change){
            $.each(change, function(index, item){
                var line = index.charAt(0).toUpperCase() + index.substring(1) + ': ' + item + "\n";
                changeField.val(changeField.val() + line);



            });
            money = 0.00;
            updateMoneyAmount();
            updateInventory();
            $('#messageDisplay').val('Thank you!');
            $('#selectedItem').val('');
            itemSelected = false;
        },

        error: function(a, b, c){
            var error = JSON.parse(a.responseText)
            $('#messageDisplay').val(error.message);
        }
    });
    }
}

function resetChange(){
    dollarsIn = 0;
    quartersIn = 0;
    dimesIn = 0;
    nickelsIn = 0;
    $('#changeDisplay').val('');
}

function updateMoneyAmount(){
$('#moneyIn').val('$' + money.toFixed(2));
}

function noItemSelectedModal(){
    $('#errorModal').modal('show');
}
        document.getElementById('decrease-quantity').addEventListener('click', function () {
            var input = document.getElementById('quantity');
        var value = parseInt(input.value, 10);
            if (value > 1) input.value = value - 1;
        });

        document.getElementById('increase-quantity').addEventListener('click', function () {
            var input = document.getElementById('quantity');
        var value = parseInt(input.value, 10);
        input.value = value + 1;
        });
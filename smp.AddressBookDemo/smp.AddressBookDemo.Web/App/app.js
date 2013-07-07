app = {};

(function (self) {

    self.RegisterCollapsibleLegend = function () {
        $('.collapsibleLegend').mouseover(function () {
            $(this).find('i').toggleClass('hide');
        });


        $('.collapsibleLegend').mouseout(function () {
            $(this).find('i').toggleClass('hide');
        });

        $('.collapsibleLegend').click(function () {

            if ($(this).find('i').hasClass('icon-chevron-down')) {
                $($(this).parent()).children('div').slideUp('500');
                $(this).find('i').toggleClass('icon-chevron-down');
                $(this).find('i').toggleClass('icon-chevron-right');
            } else {
                $($(this).parent()).children('div').slideDown('500');
                $(this).find('i').toggleClass('icon-chevron-down');
                $(this).find('i').toggleClass('icon-chevron-right');
            }
        });
    };

    self.ShowError = function(title, message) {
        $('#errorTitle').text(title);
        $('#ErrorMessage').text(message);
        $('#errorModal').modal();
    };

    self.AjaxTypeAhead = function(el, url, selectedCallback) {

        $(el).typeahead({
            source: function(query, process) {
                var data = { QueryString: query };
                $.ajax({
                    url: url,
                    type: 'POST',
                    data: data,
                    dataType: 'json',
                    success: function(result) {
                        var names = [];
                        map = { };
                        $.each(result, function(i, item) {
                            map[item.Value] = item;
                            names.push(item.Value);
                        });
                        process(names);
                    }
                });
            },
            sorter: function(items) {
                return items.sort();
            },
            updater: function(item) {
                var selectedId = map[item].Id;
                if (typeof(selectedCallback) == "function") {
                    selectedCallback(selectedId);
                }
                return item;
            }
        });
    };

})(app);



//Taken from the knockout.js documentation website.
ko.bindingHandlers.slideVisible = {
    init: function (element, valueAccessor) {
        var value = ko.utils.unwrapObservable(valueAccessor()); // Get the current value of the current property we're bound to
        $(element).toggle(value); // jQuery will hide/show the element depending on whether "value" or true or false
    },
    update: function (element, valueAccessor, allBindingsAccessor) {
        // First get the latest data that we're bound to
        var value = valueAccessor(), allBindings = allBindingsAccessor();

        // Next, whether or not the supplied model property is observable, get its current value
        var valueUnwrapped = ko.utils.unwrapObservable(value);

        // Grab some more data from another binding property
        var duration = allBindings.slideDuration || 250; // 400ms is default duration unless otherwise specified

        // Now manipulate the DOM element
        if (valueUnwrapped == true)
            $(element).slideDown(duration); // Make the element visible
        else
            $(element).slideUp(duration);   // Make the element invisible
    }
};
(function ($) {
    'use strict';

    function getParent(rows, node) {
        var parent = node.attr('data-parent');
        if (typeof parent === "string") {
            if (isNaN(parent)) {
                parent = $(parent);
            } else {
                parent = $(rows[parseInt(parent, 10)]);
            }
            return parent;
        }
        return undefined;
    }

    function renderItem(item, parent) {
        if (parent) {
            parent.find('.treegrid-node > span.expand-icon')
                .toggleClass('fa-plus-circle', parent.hasClass('collapsed'))
                .toggleClass('fa-minus-circle', !parent.hasClass('collapsed'));
            item.toggleClass('hidden', parent.hasClass('collapsed'));
            if (parent.hasClass('collapsed')) {
                item.addClass('collapsed');
            }
        }
    }

    function reStripe(tree) {
        tree.find('tbody > tr').removeClass('odd');
        tree.find('tbody > tr:not(.hidden):odd').addClass('odd');
    }

    $.fn.treegrid = function (options) {
        var i;
        var rows = this.find('tbody > tr');
        var _this = this;
        $.each(rows, function () {
            var node = $(this);
            var parent = getParent(rows, node);
            // Append expand icon
            node.children('.treegrid-node').prepend('<span class="icon expand-icon fa"/>');
            // Set up an event listener for the node
            node.children('.treegrid-node').on('click', function (e) {
                var icon = node.find('span.expand-icon');
                if (options && typeof options.callback === 'function') {
                    options.callback(e);
                }
                if (icon.hasClass('fa-plus-circle')) {
                    node.removeClass('collapsed');
                }
                if (icon.hasClass('fa-minus-circle')) {
                    node.addClass('collapsed');
                }
                $.each(rows.slice(rows.index(node) + 1), function () {
                    renderItem($(this), getParent(rows, $(this)));
                });
                reStripe(_this);
            });
            if (parent) {
                // Calculate indentation depth
                i = parent.find('.treegrid-node > span.indent').length + 1;
                for (; i > 0; i -= 1) {
                    node.children('.treegrid-node').prepend('<span class="indent"/>');
                }
                // Render expand/collapse icons
                renderItem(node, parent);
            }
        });
        reStripe(_this);
    };

    $.fn.collapseAll = function (options) {
        var rows = this.find('tbody > tr');
        $.each(rows, function () {
            var node = $(this);
            var parent = getParent(rows, node);
            var icon = node.find('span.expand-icon');
            if (icon.hasClass('fa-minus-circle')) {
                node.addClass('collapsed');
            }
            $.each(rows.slice(rows.index(node) + 1), function () {
                renderItem($(this), getParent(rows, $(this)));
            });
        });
        reStripe(this);
    };

    $.fn.expandAll = function (options) {
        var rows = this.find('tbody > tr');
        $.each(rows, function () {
            var node = $(this);
            var parent = getParent(rows, node);
            var icon = node.find('span.expand-icon');
            if (icon.hasClass('fa-plus-circle')) {
                node.removeClass('collapsed');
            }
            $.each(rows.slice(rows.index(node) + 1), function () {
                renderItem($(this), getParent(rows, $(this)));
            });
        });
        reStripe(this);
    };
}(jQuery));
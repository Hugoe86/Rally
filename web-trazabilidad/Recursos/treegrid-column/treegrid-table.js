(function ($) {
    'use strict'

    $.fn.tree = function (options) {
        var element = $(this)[0];
        var elementType = element.nodeName;
        var generated = '';

        if (elementType === 'DIV') {
            element.classList.add("table-responsive");
            generated = '<table class="table table-bordered table-hover">';
        } else if (elementType === 'TABLE') {
            element.classList.add("table");
            element.classList.add("table-bordered");
            element.classList.add("table-hover");
        }

        var render = renderTable(options.data, options.table, generated);

        if (elementType === 'DIV') {
            generated = +'</table>';
        } else if (elementType === 'TABLE') {

        }

        var jQueryElement = $('#' + element.id);
        jQueryElement.html('');
        jQueryElement.append(render);
        jQueryElement.treegrid();
    };

    function renderTable(data, table, generated) {
        generated +=
            '<thead>' +
            '<tr>';

        $.each(table.columns, function (index, value) {
            if (value.isShow) {
                generated +=
                    '<th>' + value.title + '</th>';
            }
        });

        generated +=
            '</tr>' +
            '</thead> ' +
            '<tbody>';

        $.each(data, function (index, value) {
            var keys = Object.keys(value);
            var columns = [];

            generated += '<tr id="parent-' + index + '" class="collapsed">';

            $.each(table.columns, function (indexCol, valueCol) {
                if (valueCol.isShow) {
                    columns.push(valueCol.field);
                }
            });

            $.each(keys, function (indexKey, valueKey) {
                if (columns.includes(valueKey)) {
                    var column = table.columns.find(x => x.field === valueKey);

                    generated +=
                        '<td' + (column.isTreeNode ? ' class="treegrid-node">' : '>') +
                        (column.isTreeNode ? '<span class="icon node-icon fa fa-folder"></span> ' : '') +
                        value[valueKey] +
                        '</td>';
                }
            });

            generated += '</tr>';

            var hasChild = false;
            var dataChild = [];

            for (var property in value) {
                if (Array.isArray(value[property]) && table.table.name === property) {
                    hasChild = true;
                    dataChild = value[property];
                    break;
                }
            }

            if (hasChild) {
                generated += renderSubTable(index, columns.length, dataChild, table.table, generated)
            }
        });

        generated += '</tbody>';

        return generated;
    };

    function renderSubTable(parent, colspan, data, table, generated) {
        var generatedSubTable =
            '<tr data-parent="#parent-' + parent + '" style="background-color:white;">' +
            '<td class="treegrid-node" colspan="' + colspan + '">' +
            '<div class="table-responsive">' +
            '<table class="table table-bordered table-hover table-treegrid">' +
            '<thead>' +
            '<tr>';

        $.each(table.columns, function (index, value) {
            if (value.isShow) {
                generatedSubTable +=
                    '<th>' + value.title + '</th>';
            }
        });

        generatedSubTable +=
            '</tr>' +
            '</thead> ' +
            '<tbody>';

        $.each(data, function (index, value) {
            var keys = Object.keys(value);
            var columns = [];

            generatedSubTable += '<tr id="parent-' + parent + '-' + index + '" class="collapsed">';

            $.each(table.columns, function (indexCol, valueCol) {
                if (valueCol.isShow) {
                    columns.push(valueCol.field);
                }
            });

            $.each(keys, function (indexKey, valueKey) {
                if (columns.includes(valueKey)) {
                    var column = table.columns.find(x => x.field === valueKey);
                    var row =

                        generatedSubTable +=
                        '<td' + (column.isTreeNode ? ' class="treegrid-node">' : '>') +
                        (column.isTreeNode ? '<span class="icon node-icon fa fa-folder"></span> ' : '') +
                        value[valueKey] +
                        '</td>';
                }
            });

            generatedSubTable += '</tr>';

            var hasChild = false;
            var dataChild = [];

            for (var property in value) {
                if (Array.isArray(value[property]) && table.table.name === property) {
                    hasChild = true;
                    dataChild = value[property];
                    break;
                }
            }

            if (hasChild) {
                generatedSubTable += renderSubTable(parent + '-' + index, columns.length, dataChild, table.table, generated)
            }
        });

        generatedSubTable +=
            '</tbody>' +
            '</table>' +
            '</div>' +
            '</td>' +
            '</tr>';

        return generatedSubTable;
    };

    $.fn.treegrid = function (options) {
        var i, rows, _this;
        rows = this.find('tbody > tr');
        _this = this;
        $.each(rows, function () {
            var node, parent;
            node = $(this);
            parent = getParent(rows, node);
            // Append expand icon dummies
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
            var node, parent;
            node = $(this);
            parent = getParent(rows, node);
            // Append expand icon dummies

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
            var node, parent;
            node = $(this);
            parent = getParent(rows, node);
            // Append expand icon dummies

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

    function pad(n, width, z) {
        z = z || '0';
        n = n + '';
        return n.length >= width ? n : new Array(width - n.length + 1).join(z) + n;
    }

    function getParent(rows, node) {
        var parent = node.attr('data-parent');

        if (typeof parent === "string") {
            if (isNaN(parent)) {
                ///////////////////bug fix//////////////////
                parent = $(parent);
                ///////////////////bug fix//////////////////
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

    function isString(value) {
        return typeof value === 'string' || value instanceof String;
    }

    function isNumber(value) {
        return typeof value === 'number' && isFinite(value);
    }

    function isArray(value) {
        return value && typeof value === 'object' && value.constructor === Array;
    }

    function isFunction(value) {
        return typeof value === 'function';
    }

    function isObject(value) {
        return value && typeof value === 'object' && value.constructor === Object;
    }

    function isNull(value) {
        return value === null;
    }

    function isUndefined(value) {
        return typeof value === 'undefined';
    }

    function isBoolean(value) {
        return typeof value === 'boolean';
    }

    function isRegExp(value) {
        return value && typeof value === 'object' && value.constructor === RegExp;
    }

    function isError(value) {
        return value instanceof Error && typeof value.message !== 'undefined';
    }

    function isDate(value) {
        return value instanceof Date;
    }

    function isSymbol(value) {
        return typeof value === 'symbol';
    }
}(jQuery));
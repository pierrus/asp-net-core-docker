(function () {
    'use strict';

    angular
        .module('app')
        .controller('TodoController', TodoController);

    TodoController.$inject = ['TodoService'];

    function TodoController(TodoService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = '.NET Core simple TodoList';
        vm.todo = {};
        vm.todos = [];

        vm.addTodo = addTodo;

        function addTodo() {
            var todo = {
                title: vm.todo.title
            }

            TodoService.addTodo(todo).then(function() {
                TodoService.getTodos().success(function(response) {
                    vm.todos = response;
                });
            });
        }

        activate();

        function activate() {
            TodoService.getTodos().success(function(response) {
                vm.todos = response;
            });
        }
    }
})();

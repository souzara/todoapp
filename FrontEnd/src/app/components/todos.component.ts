import { Component, OnInit } from '@angular/core';
import { TodoService } from '../services/todos.service';
import { Todo } from '../models/todo';
@Component({
    selector: 'todos',
    templateUrl: 'todos.component.html'
})

export class TodosComponent implements OnInit {
    public todos: Todo[] = [];
    public inEditMode: boolean = false;
    public currentTodo: Todo;
    public buttonLabel: string = 'Create';

    constructor(private service: TodoService) {
        this.currentTodo = new Todo();
    }

    public remove(id: number): void {
        this.service.delete(id)
            .subscribe((res) => {
                if (res.success) {
                    this.loadTodos();
                }
                else {
                    console.error(res.errors);
                }
            });
    }

    public edit(todo: Todo): void {
        this.currentTodo = todo;
        this.inEditMode = true;
        this.buttonLabel = 'Save';
    }

    public setOrUnsetCompleted(todo: Todo): void {
        todo.isCompleted = !todo.isCompleted;
        this.service.put(todo.id, todo)
            .subscribe((res) => {
                if (!res.success) {
                    console.error(res.errors);
                }
            });
    }

    public cancel(): void {
        this.currentTodo = new Todo();
        this.inEditMode = false;
    }


    public save(): void {
        if (!this.inEditMode) {
            this.saveNewTodo();
        } else {
            this.updateTodo();
        }
    }

    private saveNewTodo(): void {
        this.service.post(this.currentTodo)
            .subscribe((res) => {
                if (res.success) {
                    this.todos.push(res.result);
                    this.currentTodo = new Todo();
                    this.inEditMode = false;
                }
                else {
                    console.error(res.errors);
                }
            });
    }

    private updateTodo(): void {
        this.service.put(this.currentTodo.id, this.currentTodo)
            .subscribe((res) => {
                if (res.success) {
                    this.currentTodo = new Todo();
                    this.inEditMode = false;
                    this.buttonLabel = 'Create';
                }
                else {
                    console.error(res.errors);
                }
            })
    }

    private loadTodos(): void {
        this.todos = [];
        this.service.list()
            .subscribe((res) => {
                if (res.success) {
                    this.todos = res.result;
                } else {
                    console.error(res.errors);
                }
            });
    }

    public ngOnInit() {
        this.loadTodos();
    }
}
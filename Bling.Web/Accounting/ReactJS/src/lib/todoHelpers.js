import _ from 'lodash';

export const addTodo = (list, item) => [...list, item];

export const generateId = () => Math.floor(Math.random()*100000);

export const findById = (id, list) => _.find(list, item => item.id === id);

export const toggleTodo = (todo) => ({...todo, isComplete: !todo.isComplete}); 

export const updateTodo = (list, updated) => {
    const updatedIndex = _.findIndex(list, item => item.id === updated.id);
    return [
        ...list.slice(0, updatedIndex),
        updated,
        ...list.slice(updatedIndex+1)
    ]
}

export const removeTodo = (list, id) => {
    const index = _.findIndex(list, item => item.id === id);
    return [
        ...list.slice(0, index),
        ...list.slice(index+1)
    ]
}

export const filterTodos = (list, route) => {
    switch (route) {
        case '/active':
            return list.filter(item => !item.isComplete);
        case '/complete':
            return list.filter(item => item.isComplete);
        default:
            return list;
    }
}
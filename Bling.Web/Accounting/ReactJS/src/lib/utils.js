export const partial = (fn, ...args) => fn.bind(null, ...args);

export const pipe2 = (f, g) => {
    return function (num) {
        return g(f(num));
    }
}

const _pipe = (f, g) => (...args) => g(f(...args));  

export const pipe = (...fns) => fns.reduce(_pipe);
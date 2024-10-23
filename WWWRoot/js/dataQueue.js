class dataQueue {
    constructor() {
        this.data = [];
    }
    //添加数据到列队中
    push(params) {
        this.data.push(params);
    }
    //获取列队中的数据
    get(index = 0) {
        return this.data[index];
    }

    getAll() {
        return this.data;
    }
    //获取最后一个元素
    getLast() {
        return this.data[this.data.length - 1];
    }
    //获取指定元素的位置
    getIndex(item) {
        return this.data.lastIndexOf(item);
    }
    //从列队中删除数据
    dequeue() {
        return this.data.shift();
    }

    //判断列队是否为空
    isEmpty() {
        return this.data.length === 0;
    }
    //列队长度
    size() {
        return this.data.length;
    }
    //清空列队
    clear() {
        this.data = [];
    }
    //打印列队
    toString() {
        return this.data.toString();
    }
}
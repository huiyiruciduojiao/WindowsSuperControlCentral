// 获取页面中的元素
const outputElement = document.getElementById('output');  // 输出结果的元素
const inputElement = document.getElementById('input');    // 输入框元素
const terminal = document.getElementById('terminal');     // 终端区域元素
let textNode = document.createTextNode(" ");               // 用于调整输入框位置的文本节点
// 获取输出区域最后一个文本节点
const lastTextNode = getLastTextNode(outputElement);
if (lastTextNode) {
    // 将文本节点添加到最后一个文本节点的父节点
    lastTextNode.parentNode.appendChild(textNode);
} else {
    // 如果没有文本节点，将文本节点添加到输出区域
    outputElement.appendChild(textNode);
}

//创建Range 对象并设置其起始和终止位置
const range = document.createRange();
range.setStart(textNode, 0);
range.setEnd(textNode, 0);

// 获取文本节点范围的边界矩形
const rect = range.getBoundingClientRect();

// 获取终端区域的边界矩形
const containerRect = terminal.getBoundingClientRect();

// 计算输入框的位置和宽度
const x = rect.x - containerRect.x;
const width = terminal.scrollWidth - x - 10;

// 设置输入框的宽度
inputElement.style.width = `${width}px`;

// 移除文本节点，以免影响布局
textNode.remove();
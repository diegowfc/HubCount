import React, { useState, useEffect } from 'react';
import Chart from 'chart.js/auto';
import axios from 'axios';

const ExibirGraficos = () => {
    const [chartData1, setChartData1] = useState(null);
    const [chartData2, setChartData2] = useState(null);
    const [listData, setListData] = useState(null);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response1 = await axios.get('https://localhost:7132/api/Pedidos/ListarVendaPorProduto');
                const response2 = await axios.get('https://localhost:7132/api/Pedidos/ListarVendaPorRegiao');
                const response3 = await axios.get('https://localhost:7132/api/Pedidos/ListarVendas');

                setChartData1(response1.data);
                setChartData2(response2.data);
                setListData(response3.data);
            } catch (error) {
                console.error('Error fetching data:', error);
            }
        };

        fetchData();
    }, []);

    useEffect(() => {
        if (chartData1) {
            const ctx1 = document.getElementById('myChart1').getContext('2d');

            const chartLabels1 = chartData1.map(item => item.produtoID);
            const chartDataValues1 = chartData1.map(item => item.count);

            const myChart1 = new Chart(ctx1, {
                type: 'doughnut',
                data: {
                    labels: chartLabels1,
                    datasets: [
                        {
                            label: 'Total de vendas',
                            backgroundColor: ['#FF6384', '#36A2EB', '#FFCE56', '#4CAF50', '#FF9800'],
                            data: chartDataValues1,
                        },
                    ],
                },
            });

            return () => {
                myChart1.destroy();
            };
        }
    }, [chartData1]);

    useEffect(() => {
        if (chartData2) {
            const ctx2 = document.getElementById('myChart2').getContext('2d');

            const chartLabels2 = chartData2.map(item => item.regiao);
            const chartDataValues2 = chartData2.map(item => item.count);

            const myChart2 = new Chart(ctx2, {
                type: 'doughnut',
                data: {
                    labels: chartLabels2,
                    datasets: [
                        {
                            label: 'Vendas por região',
                            backgroundColor: ['#FF6384', '#36A2EB', '#FFCE56', '#4CAF50', '#FF9800'],
                            data: chartDataValues2,
                        },
                    ],
                },
            });

            return () => {
                myChart2.destroy();
            };
        }
    }, [chartData2]);

    return (
        <div>
            <div id="myChartContainer1" style={{ maxWidth: '400px', margin: '20px' }}>
                <canvas id="myChart1" width="200" height="200"></canvas>
            </div>
            <div id="myChartContainer2" style={{ maxWidth: '400px', margin: '20px' }}>
                <canvas id="myChart2" width="200" height="200"></canvas>
            </div>

            {listData && (
                <div style={{ position: 'absolute', top: '80px', right: '20px', maxWidth: '1500px' }}>
                    <h2>Lista de vendas</h2>
                    <ul>
                        {listData.map(item => (
                            <li key={item.id}>
                                <strong>Nome:</strong> {item.nome}, &nbsp;
                                <strong>Produto:</strong> {item.produto}, &nbsp;
                                <strong>Total:</strong> {item.valor}, &nbsp;
                                <strong>Entrega:</strong> {item.data}
                            </li>
                        ))}
                    </ul>
                </div>
            )}
        </div>
    );
};

export default ExibirGraficos;


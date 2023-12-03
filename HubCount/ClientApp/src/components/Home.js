import React, { Component } from 'react';
import { Link } from 'react-router-dom';

export class Home extends Component {
    static displayName = Home.name;

    render() {
        return (
            <div>
                <h1>Seja bem-vindo à Onion S.A.!</h1>
                <p>Nós somos uma empresa líder na indústria de eletrônicos, dedicada à inovação e tecnologia. Nossos produtos são eletrônicos como celular, smart tvs e notebooks.</p>
                <p>Confira nossas funcionalidades:</p>
                <ul>
                    <li><Link className="text-dark" to="/ImportarExcel">Importar Excel</Link></li>
                    <li><Link className="text-dark" to="/ExibirGraficos">Exibir gráficos</Link></li>
                </ul>
            </div>
        );
    }
}

import React, { Component } from 'react';

export class ImportarExcel extends Component {
    static displayName = ImportarExcel.name;

    constructor(props) {
        super(props);
        this.state = {
            selectedFile: null,
            showFileWarning: false,
            importResultMessage: null,
            isLoading: false,
        };
    }

    handleFileChange = (event) => {
        this.setState({ selectedFile: event.target.files[0], showFileWarning: false });
    };

    handleSubmit = async () => {
        if (!this.state.selectedFile) {
            this.setState({ showFileWarning: true, importResultMessage: null });
            return;
        }

        this.setState({ isLoading: true });

        const formData = new FormData();
        formData.append('ExcelFile', this.state.selectedFile);

        try {
            const response = await fetch('/ImportarExcel', {
                method: 'POST',
                body: formData,
            });

            if (response.ok) {
                this.setState({
                    importResultMessage: 'Arquivo importado com sucesso!',
                    showFileWarning: false,
                });
            } else {
                this.setState({
                    importResultMessage: 'Ocorreu um erro ao importar o arquivo!',
                    showFileWarning: false,
                });
            }
        } catch (error) {
            this.setState({
                importResultMessage: `Atenção!: ${error.message}`,
                showFileWarning: false,
            });
        } finally {
            this.setState({ isLoading: false });
        }
    };

    render() {
        return (
            <div>
                <h1>Importação de pedidos por planilha</h1>

                <p>
                    Importe sua planilha (como disponibilizada no{' '}
                    <a
                        href="https://docs.google.com/spreadsheets/d/e/2PACX-1vQdYUswAMw9SXFXIyHeFzytOwm_A-S1ydEo6bGIvMr1bhmbyvj_mwWGuJUQkh5rRA/pub?output=xlsx"
                        target="_blank"
                        rel="noopener noreferrer"
                    >
                        exemplo
                    </a>
                    ) para exibir gráficos com as informações sobre:
                </p>

                <ul>
                    <li>Gráfico de vendas por região</li>
                    <li>Gráfico de vendas por produto</li>
                    <li>Relatório de venda final</li>
                </ul>

                <div>
                    <label htmlFor="fileInput">Importe seu arquivo:</label>
                    <br></br>
                    <input type="file" id="fileInput" onChange={this.handleFileChange} />
                </div>

                {this.state.showFileWarning && (
                    <p style={{ color: 'red' }}>Selecione um arquivo antes de gerar os gráficos.</p>
                )}

                {this.state.importResultMessage && (
                    <p style={{ color: this.state.importResultMessage.includes('Error') ? 'red' : 'green' }}>
                        {this.state.importResultMessage}
                    </p>
                )}
                <br></br>
                <button
                    className="btn btn-primary"
                    onClick={this.handleSubmit}
                    disabled={this.state.isLoading}
                >
                    {this.state.isLoading ? 'Carregando...' : 'Gerar gráficos'}
                </button>
            </div>
        );
    }
}

const API_URL = "https://localhost:5001";

async function sendRequest(endpoint, method, body = null, authRequired = true) {
    const token = localStorage.getItem("token");
    const headers = {};

    if (authRequired && token)
        headers["Authorization"] = `Bearer ${token}`;

    headers["Content-Type"] = "application/json";

    let fetchBody = null;

    if (body && method !== "GET")
        fetchBody = JSON.stringify(body);

    const makeRequest = async () => {

        const response = await fetch(`${API_URL}/${endpoint}`, {
            method: method,
            headers: headers,
            body: fetchBody
        });

        let data = {};

        try {
            data = await response.json();
        }
        catch
        {
        }

        return { response, data };
    };

    try {
        let { response, data } = await makeRequest();

        if (response.status === 401) {
            alert("Não autorizado");
            return null;
        }

        if (response.status === 400 || response.status > 401) {
            const mensagem = data?.erro || data?.message || "Erro desconhecido";

            alert(mensagem);
            return null;
        }

        return data;
    } catch (error) {
        alert("Erro de conexão com a API");
        return null;
    }
}
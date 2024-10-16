using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMovement : MonoBehaviour
{
    private Transform alvo; // O alvo ser� buscado automaticamente

    [SerializeField]
    private float moveSpeed = 2.0f;

    [SerializeField]
    private float dashSpeed = 10.0f; // Velocidade do dash

    [SerializeField]
    private float dashDistance = 3.0f; // Dist�ncia para come�ar o dash

    [SerializeField]
    private float dashOverflow = 2.0f; // Quanto al�m do player o dash deve seguir

    [SerializeField]
    private float chargeTime = 1.0f; // Tempo de carregamento antes do dash

    [SerializeField]
    [Range(0, 1)]
    private float interpolation = 0.1f;

    private Vector2 targetPosition;

    private bool isDashing = false;
    private bool isCharging = false;

    private LineRenderer lineRenderer;

    void Start()
    {
        // Encontra o objeto com a tag "Player" e o define como alvo
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            alvo = playerObject.transform;
        }
        else
        {
            Debug.LogError("Nenhum objeto com a tag 'Player' foi encontrado. Por favor, certifique-se de que o jogador tenha a tag correta.");
            return; // Evita executar o resto do c�digo se n�o houver um alvo
        }

        // Adiciona e configura o LineRenderer para desenhar a linha de trajet�ria
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.5f;
        lineRenderer.endWidth = 0.5f;
        lineRenderer.positionCount = 2;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // Shader simples para a linha
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;
        lineRenderer.enabled = false; // Inicialmente a linha n�o � exibida

        // Configura a camada de sorting para garantir que a linha fique abaixo do player
        lineRenderer.sortingLayerName = "Background"; // Certifique-se de ter uma camada de sorting chamada "Background"
        lineRenderer.sortingOrder = -1; // Ordem de sorting mais baixa para garantir que esteja por tr�s do player
    }

    void Update()
    {
        if (alvo == null) return; // Certifique-se de que h� um alvo v�lido antes de continuar

        // Verifica a dist�ncia entre o rob� e o alvo
        float distanceToTarget = Vector2.Distance(transform.position, alvo.position);

        if (distanceToTarget <= dashDistance && !isDashing && !isCharging)
        {
            // Se estiver dentro da dist�ncia, inicia o carregamento do dash
            isCharging = true;
            StartCoroutine(ChargeDash());
        }
        else if (!isDashing && !isCharging)
        {
            // Movimenta-se em dire��o ao alvo com velocidade normal
            targetPosition = alvo.position;
            Movement();
        }
    }

    IEnumerator ChargeDash()
    {
        // Calcula a dire��o do dash e a posi��o final do dash com overflow
        Vector2 directionToTarget = (alvo.position - transform.position).normalized;
        Vector2 dashTarget = (Vector2)alvo.position + directionToTarget * dashOverflow;

        // Ativa e define os pontos do LineRenderer para mostrar a trajet�ria
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, dashTarget);

        // Espera o tempo de carregamento antes de iniciar o dash
        yield return new WaitForSeconds(chargeTime);

        // Desativa a linha e inicia o dash
        lineRenderer.enabled = false;
        isDashing = true;
        StartCoroutine(DashTowardsTarget(dashTarget));
    }

    IEnumerator DashTowardsTarget(Vector2 dashTarget)
    {
        float dashTime = 0.3f; // Tempo do dash
        float elapsedTime = 0f;

        Vector2 startPosition = transform.position;

        while (elapsedTime < dashTime)
        {
            // Move do ponto inicial ao ponto final usando uma interpola��o linear
            transform.position = Vector2.Lerp(startPosition, dashTarget, (elapsedTime / dashTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Certifica que o dash finaliza exatamente na posi��o do alvo + overflow
        transform.position = dashTarget;
        isDashing = false;
        isCharging = false;
    }

    void Movement()
    {
        // Movimenta o rob� suavemente em dire��o ao alvo enquanto n�o estiver dando dash
        transform.position = Vector2.Lerp(transform.position, targetPosition, interpolation * Time.deltaTime * moveSpeed);
    }
}
